using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummy : Enemy
{
    [Header("Scene/Asset References")]
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private GameObject arm = null;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private Transform rotator = null;

    [Header("Move Variables")]
    [SerializeField] private float restDuration = 0;
    [SerializeField] private int numberOfShots = 0;
    [SerializeField] private float timeBetweenShots = 0;
    [SerializeField] private int numberOfSwings = 0;
    [SerializeField] private float timeBetweenSwings = 0;
    [SerializeField] private float swingSpeed = 0;
    [SerializeField] private float swingTime = 0;

    private MovePool restPool = new MovePool();

    private int shotsLeft = 0;
    private int swingsLeft = 0;
    private Vector3 armPos = Vector3.zero;
    private Quaternion armRot = Quaternion.identity;

    protected override void Awake()
    {
        base.Awake();
        InitializeAI();
    }

    private void Update()
    {
        if (player)
        {
            aiTimer -= Time.deltaTime;
            allMoves.Move(aiState);
        }
    }

    protected override void InitializeAI()
    {
        shotsLeft = numberOfShots;
        swingsLeft = numberOfSwings;
        armPos = arm.transform.position;
        armRot = arm.transform.rotation;
        arm.GetComponent<Stick>().swingSpeed = swingSpeed;
    }

    protected override void SetUpMovePools()
    {
        restPool.MergePool(
            new Dictionary<string, Action>()
            {
                { "shoot", Shoot },
                { "swing", Swing }
            }
        );

        allMoves.MergePool(
            new Dictionary<string, Action>()
            {
                { "rest", Rest }
            }
        );
        allMoves.MergePool(restPool);
    }

    private void Rest()
    {
        SetUpMove("rest", restDuration);

        if (aiTimer <= 0)
        {
            moveInit = true;
            restPool.RandomMove();
        }
    }

    private void Shoot()
    {
        SetUpMove("shoot", shotsLeft == numberOfShots ? 0 : timeBetweenShots);

        if (aiTimer <= 0)
        {
            Vector2 direction = (player.transform.position - rotator.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rotator.eulerAngles = Vector3.forward * angle;

            Bullet newBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation).GetComponent<Bullet>();
            newBullet.SetOrigin(gameObject);

            shotsLeft--;
            moveInit = true;
        }

        if (shotsLeft == 0)
        {
            moveInit = true;
            shotsLeft = numberOfShots;
            Rest();
        }       
    }

    private void Swing()
    {
        SetUpMove("swing", swingsLeft == numberOfSwings ? 0 : timeBetweenSwings + swingTime);

        // If parried, increase time before next attack

        if (aiTimer <= 0)
        {
            arm.SetActive(true);
            StartCoroutine(SwingArm());

            swingsLeft--;
            moveInit = true;
        }

        if (swingsLeft == 0)
        {
            moveInit = true;
            Rest();
        }
    }

    private void ResetArm()
    {
        arm.transform.position = armPos;
        arm.transform.rotation = armRot;
    }

    IEnumerator SwingArm()
    {
        arm.GetComponent<Stick>().swingingForward = true;

        yield return new WaitForSeconds(swingTime);

        arm.GetComponent<Stick>().swingingForward = false;
        arm.SetActive(false);
        ResetArm();
    }
}
