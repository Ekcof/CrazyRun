using UnityEngine;

public class TouchControl : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float maxZoomIn = 20f;
    [SerializeField] private float maxZoomOut = 100f;
    [SerializeField] private GameObject virtualCamera;
    private CameraMovement cameraMovement;
    private float distance;
    private Vector3 newPos;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 startPos2;
    private Vector2 endPos2;
    private Touch touch;
    private Touch touch2;

    private void Awake()
    {
        cameraMovement = virtualCamera.GetComponent<CameraMovement>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) startPos = touch.position;
            switch (Input.touchCount)
            {
                case 1:
                    GetSwipe();
                    break;
                case 2:
                    GetZoom();
                    break;
            }

        }
    }

    public Vector2 GetDeltaPos()
    {
        return Input.GetTouch(0).deltaPosition;
    }


    public bool IsSingleTouchActive()
    {
        if (Input.touchCount == 1)
        {
            return true;
        }
        return false;
    }

    // Get if there is a zoom
    private void GetZoom()
    {
        touch2 = Input.GetTouch(1);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                startPos = touch.position;
                break;
            case TouchPhase.Moved:
                endPos = touch.position;
                break;
        }
        switch (touch2.phase)
        {
            case TouchPhase.Began:
                startPos2 = touch2.position;
                break;
            case TouchPhase.Moved:
                endPos2 = touch2.position;
                break;
        }

        float distanceStart = Vector3.Distance(new Vector3(startPos.x, 0, startPos.y), new Vector3(startPos2.x, 0, startPos2.y));
        float distanceEnd = Vector3.Distance(new Vector3(endPos.x, 0, endPos.y), new Vector3(endPos2.x, 0, endPos2.y));
        bool xIsUp = (startPos.x < endPos.x);
        bool xIsUp2 = (startPos2.x < endPos2.x);
        bool yIsUp = (startPos.y < endPos.y);
        bool yIsUp2 = (startPos2.y < endPos2.y);

        if ((xIsUp != xIsUp2) && (yIsUp != yIsUp2)) cameraMovement.ChangeRelation(distanceEnd/ distanceStart);
    }

    // Get if there is a swipe
    private void GetSwipe()
    {
        if (touch.phase == TouchPhase.Ended) endPos = touch.position;
        if (Input.touchCount == 1)
        {
            Vector2 swipeMove = startPos - endPos;
            float x = Mathf.Abs(swipeMove.x);
            float y = Mathf.Abs(swipeMove.y);
            if (x > 100 && y < 50) Debug.Log("Good Swipe!");
        }
    }

}
