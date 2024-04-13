using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActiveItem : Item
{
    public int Level;
    public float Radius;
    public Projection Projection;

    [SerializeField] protected TextMeshProUGUI _levelText;
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected SphereCollider _trigger;

    public Rigidbody Rigidbody;
    public bool IsDead;

    [SerializeField] protected Animator _animator;

    [ContextMenu("IncreaseLevel")]

    protected virtual void Start()
    {
        Projection.Hide();
    }
    public void IncreaseLevel()
    {
        Level++;
        SetLevel(Level);
        _animator.SetTrigger("IncreaseLevel");

        _trigger.enabled = false;
        Invoke(nameof(Enabletrigger), 0.08f);
    }

    public void DecreaseLevel()
    {
        if (Level < 1)
        {
            Die();
            return;
        }

        Level--;
        SetLevel(Level);
        _animator.SetTrigger("IncreaseLevel");

        _trigger.enabled = false;
        Invoke(nameof(Enabletrigger), 0.08f);
    }

    public virtual void SetLevel(int level)
    {
        Level = level;
        int number = (int)Mathf.Pow(2, level + 1);
        string numberString = number.ToString();
        _levelText.text = numberString;
    }

    void Enabletrigger()
    {
        _trigger.enabled = true;
    }

    public void SetupToTube()
    {
        _trigger.enabled = false;
        _collider.enabled = false;
        Rigidbody.isKinematic = true;
        Rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    public void Drop()
    {
        _trigger.enabled = true;
        _collider.enabled = true;
        Rigidbody.isKinematic = false;
        Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        transform.parent = null;
        Rigidbody.velocity = Vector3.down * 1.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsDead) return;
        if (other.attachedRigidbody)
        {
            ActiveItem otherItem = other.attachedRigidbody.GetComponent<ActiveItem>();
            if (otherItem)
            {
                if (!otherItem.IsDead && Level == otherItem.Level)
                {
                    CollapseManager.instance.Collapse(this, otherItem);
                }
            }
        }
    }

    public void Disable()
    {
        _trigger.enabled = false;
        Rigidbody.isKinematic = true;
        _collider.enabled= false;
        IsDead = true;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public virtual void DoEffect()
    {

    }
}
