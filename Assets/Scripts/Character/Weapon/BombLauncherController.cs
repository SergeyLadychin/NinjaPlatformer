using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLauncherController : Weapon, IPickable
{
    private LineRenderer lineRenderer;
    private const int segmentCount = 50;
    private Vector3 velocity;
    private Vector3 parentPosition;

    public int count;
    public float flyTime = 1.0f;
    public Sprite targetSprite;
    public GameObject bomb;
    public Vector2 positionOffset;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segmentCount + 1;
        parentPosition = transform.parent.position;
    }

    void OnEnable()
    {
        //Reset positions to avoid flickering after activation/deactivation
        //and for correct initialization, when object first time was activated
        ResetPositions();
    }

    void Update()
    {
        var endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        endPosition.z = transform.position.z;
        UpdateStartPosition(endPosition);
        var startPosition = transform.position;
        
        velocity = CalculateStartVelocity(startPosition, endPosition);

        UpdateLine(new [] { startPosition, endPosition}, velocity * flyTime);
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
    }

    public override void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public override float OnFire()
    {
        animator.SetBool("Throw", true);
        return cooldown;
    }

    public override void Fire()
    {
        var bombObject = Instantiate(bomb, transform.position, Quaternion.identity);
        var bombController = bombObject.GetComponent<BombController>();
        bombController.ThrowBomb(velocity);
        animator.SetBool("Throw", false);
        count--;
    }

    public override bool IsAvaliable()
    {
        return count != 0;
    }

    public void Add()
    {
        count++;
    }

    private Vector3 CalculateStartVelocity(Vector3 startPosition, Vector3 endPosition)
    {
        var angle = Mathf.Atan2(endPosition.y - startPosition.y + 0.5f * Mathf.Abs(Physics2D.gravity.y) * flyTime * flyTime, endPosition.x - startPosition.x);
        var velocity =  Mathf.Abs(endPosition.x - startPosition.x) / (flyTime * Mathf.Abs(Mathf.Cos(angle)));
        return Quaternion.Euler(0, 0, angle * 180f / Mathf.PI) * Vector3.right * velocity;
    }

    private void UpdateLine(Vector3[] curvePoints, Vector3 tangent)
    {
        float segmentLength = 1.0f / segmentCount;
        float fraction = 0.0f;

        //control points for Bezier curve interpolation
        //var controlPoints = new Vector3[] { curvePoints[0], curvePoints[0] + 0.5f * tangent, curvePoints[1] };
        for (int i = 0; i < segmentCount; i++)
        {
            lineRenderer.SetPosition(i, GetHermiteCurveInterpolation(curvePoints, tangent, fraction));
            fraction += segmentLength;
        }
        lineRenderer.SetPosition(segmentCount, GetHermiteCurveInterpolation(curvePoints, tangent, 1.0f));
    }

    private Vector3 GetBezierCurveInterpolation(Vector3[] controlPoints, float u)
    {
        return controlPoints[0] * (1 - u) * (1 - u) + 2 * controlPoints[1] * u * (1 - u) + controlPoints[2] * u * u;
    }

    private Vector3 GetHermiteCurveInterpolation(Vector3[] positions, Vector3 tangent, float u)
    {
        return (positions[1] - positions[0] - tangent) * u * u + tangent * u + positions[0];
    }

    private void ResetPositions()
    {
        for (int i = 0; i < segmentCount + 1; i++)
        {
            lineRenderer.SetPosition(i, transform.position);
        }
    }

    private void UpdateStartPosition(Vector2 endPosition)
    {
        var facingDirection = controller.GetFacingDirection();
        var targetSide = endPosition - (Vector2) parentPosition;
        var dotProduct = Vector2.Dot(targetSide, facingDirection);
        if (dotProduct > 0)
        {
            transform.localPosition = new Vector3(positionOffset.x, positionOffset.y, transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(-1 * positionOffset.x, positionOffset.y, transform.localPosition.z);
        }
    }
}
