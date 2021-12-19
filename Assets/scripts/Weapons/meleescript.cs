using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleescript : WeaponClass
{


    [SerializeField]
    protected string leftPunchAnim;

    [SerializeField]
    protected string rightPunchAnim;

    [SerializeField]
    protected string idle;

    [SerializeField]
    protected string inspect;

    [SerializeField]
    protected float inspectTime;

    [SerializeField]
    protected AudioClip punch1;

    [SerializeField]
    protected AudioClip punch2;

    [SerializeField]
    protected AudioClip inspectSound;

    [SerializeField]
    protected float hitRadius;

    [SerializeField]
    protected float damage;

    [SerializeField]
    protected float force;

    [SerializeField]
    protected float hitDelay;

    protected float hitTimer;
    protected bool hasHit;

    protected override void Start()
    {
        base.Start();

        sounds.Add(punch1);
        sounds.Add(punch2);
        sounds.Add(inspectSound);

        hitTimer = hitDelay;
        if(viewmodel.activeSelf)
        {
            ViewAnim.SetFloat("idleTimer", inspectTime);
        }
    }

    protected override void Update()
    {
        hitTimer -= Time.fixedDeltaTime;
        if(viewmodel.activeSelf)
        {
            if (ViewAnim.GetCurrentAnimatorStateInfo(0).IsName(idle))
            {
                ViewAnim.SetFloat("idleTimer", ViewAnim.GetFloat("idleTimer") - Time.fixedDeltaTime);
            }
            else
            {
                ViewAnim.SetFloat("idleTimer", inspectTime);
            }

            if (!fire1Down && !fire2Down)
            {
                hasHit = false;
            }

            if (!hasHit && hitTimer <= 0)
            {
                if (fire1Down)
                {
                    leftPunch();
                    hasHit = true;
                    hitTimer = hitDelay;
                }
                else if (fire2Down)
                {
                    rightPunch();
                    hasHit = true;
                    hitTimer = hitDelay;
                }
            }
        }
    }

    protected void rightPunch()
    {
        ViewAnim.Play(rightPunchAnim);
        playsound(sounds.IndexOf(punch1));
        smack(damage, 1f, hitRadius, Shootable);
    }

    protected void leftPunch()
    {
        ViewAnim.Play(leftPunchAnim);
        playsound(sounds.IndexOf(punch2));
        smack(damage, 1f, hitRadius, Shootable);
    }
}
