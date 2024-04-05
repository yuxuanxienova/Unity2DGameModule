using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorControl : MonoBehaviour
{
    public GameObject searchRangeIndicator;
    public GameObject combatRangeIndicator;
    public GameObject movePositionIndicator;
    public GameObject combatLowerRangeIndicator;
    private IMovePosition imovePosition;
    private float searchRange;
    private float combatRange;
    private float combatLowerRange;
    private Animator animator;
    private bool isSet;
    private GameObject target;
    private Vector3 targetPosPredict;

    public void SetTargetPosPredict(Vector3 _targetPosPredict)
    {
        Debug.Log("SetTargetPosPredict");
        targetPosPredict = _targetPosPredict;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        searchRange = animator.GetFloat("SearchRange");
        combatRange = animator.GetFloat("CombatRange");
        combatLowerRange = animator.GetFloat("CombatLowerRange");
        isSet = false;
        imovePosition = GetComponent<IMovePosition>();
        targetPosPredict = new Vector3 (0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        SetRangeIndicator();
        
    }
    public void SetRangeIndicator()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isSet = !isSet;
            searchRangeIndicator.SetActive(isSet);
            searchRangeIndicator.transform.localScale = new Vector3( searchRange*2, searchRange*2, 1);
            combatRangeIndicator.SetActive(isSet);
            combatRangeIndicator.transform.localScale = new Vector3( combatRange*2, combatRange*2, 1);
            movePositionIndicator.SetActive(isSet);
            combatLowerRangeIndicator.SetActive(isSet);

                       
        } 
        //continue updating
        movePositionIndicator.transform.position = imovePosition.GetMovePosition();
        combatLowerRangeIndicator.transform.position = targetPosPredict;
        combatLowerRangeIndicator.transform.localScale = new Vector3( combatLowerRange*2, combatLowerRange*2, 1);



    }
}
