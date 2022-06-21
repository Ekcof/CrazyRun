using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float maxZoomdistanceIn = 2f;
    [SerializeField] private float maxZoomdistanceOut = 10f;
    //private Vector3 direction;
    private float distance;
    private float topDistance;
    private Vector3 topVector3;
    //private Vector3 newOffset;
    private Vector3 standardOffset;
    private float relation = 1.0f;
    private float maxInLimitRelation = 0f;
    private float maxOutLimitRelation = 1f;
    private float backDistance;
  
    private void Awake()
    {

        distance = Vector3.Distance(transform.position, playerTransform.position);
        backDistance = maxZoomdistanceOut - distance;
        topVector3 = transform.position - (-transform.forward * -backDistance);
        topDistance = Vector3.Distance(topVector3, playerTransform.position);
        relation = distance / topDistance;
        standardOffset = transform.position - playerTransform.position;
        ResetLimitedRelations();
    }

    private void FixedUpdate()
    {
        topVector3 = playerTransform.position + standardOffset - (-transform.forward * -backDistance);
        transform.position = Vector3.Lerp(playerTransform.position, topVector3, relation);
        distance = Vector3.Distance(transform.position, playerTransform.position);
        Debug.DrawLine(playerTransform.position, topVector3);
    }

    private void ResetLimitedRelations()
    {
        distance = Vector3.Distance(transform.position, playerTransform.position);
        maxInLimitRelation = maxZoomdistanceIn / topDistance;
        //maxOutLimitRelation = maxZoomdistanceOut / topDistance;
    }

    public void ChangeRelation(float newRelation)
    {
        relation *= Mathf.Sqrt(1 / newRelation);
        if (relation < maxInLimitRelation)
        {
            relation = maxInLimitRelation;
        }
        //else if (relation > maxOutLimitRelation)
        //{
        //    relation = maxOutLimitRelation;
        //}
    }
}
