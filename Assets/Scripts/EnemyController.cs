using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Gaming attributes")] [SerializeField]
    private float speed;

    [SerializeField] private float speedOfAttack;
    [SerializeField] private int damage;
    [SerializeField] private int deathReward;
    public int probabilityOfOccurrence = 50;

    [Header("Prefab settings")] [SerializeField]
    private GameObject partToOrient;

    private Vector3 directionOfMotion;
    [NonSerialized] public Vector3 spawnPosition;
    private HPController HPOfAttackedTarget;
    private bool isAttack;
    private GameController gameController;
    private PlayerController playerController;
    private Animator animator;
    [SerializeField] private GameObject enemyDieEffect;

    private enum State
    {
        Walk,
        Attack,
        Die
    }

    private void Start()
    {
        gameController = GameController.instance;
        playerController = PlayerController.instance;
        animator = gameObject.GetComponent<Animator>();
        directionOfMotion = -spawnPosition;
        partToOrient.transform.rotation = Quaternion.LookRotation(-directionOfMotion);
        currentState = State.Walk;
    }

    private State currentState;
    private static readonly int Die1 = Animator.StringToHash("die");
    private static readonly int Attack1 = Animator.StringToHash("attack");

    private void FixedUpdate()
    {
        UpdateState();
        if (HPOfAttackedTarget == null)
        {
            currentState = State.Walk;
        }
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case State.Walk:
                Walk();
                break;
            case State.Attack:
                StartAttack();
                break;
            case State.Die:
                Die();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Walk()
    {
        animator.SetBool(Attack1, false);
        if (transform != null) transform.Translate(directionOfMotion * speed * Time.deltaTime);
    }

    private void Attack()
    {
        if (animator != null) animator.SetBool(Attack1, true);
        HPOfAttackedTarget.takeDamage(damage);
    }

    private void StartAttack()
    {
        if (isAttack) return;
        InvokeRepeating(nameof(Attack), 0f, speedOfAttack);
        isAttack = true;
    }

    public void Die()
    {
        if (animator != null) animator.SetTrigger(Die1);
        Destroy(gameObject);
        playerController.increaseMoney(deathReward);
        gameController.UpdateNumOfEnemy();
        var spawnedEffect = Instantiate(enemyDieEffect, transform.position, Quaternion.identity);
        Destroy(spawnedEffect, 2f);
    }

    private void OnTriggerEnter(Collider other)

    {
        if (!other.CompareTag("Tower") && !other.CompareTag("Turret")) return;
        HPOfAttackedTarget = other.GetComponent<HPController>();
        currentState = State.Attack;
    }
}