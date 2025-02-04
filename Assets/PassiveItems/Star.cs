using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : ActiveItem
{
    [Header("Star")]
    [SerializeField] private float _affectRadius = 1.5f;

    [SerializeField] private GameObject _affectArea;
    [SerializeField] private GameObject _affectPrefab;

    protected override void Start()
    {
        base.Start();
        _affectArea.SetActive(false);
    }


    private IEnumerator AffectProcess()
    {
        _affectArea.SetActive(true);
        _animator.enabled = true;
        yield return new WaitForSeconds(1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _affectRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].attachedRigidbody && colliders[i].isTrigger)
            {
                ActiveItem item = colliders[i].attachedRigidbody.GetComponent<ActiveItem>();
                if (item && item.ItemType == ItemType.Ball)
                {
                    item.IncreaseLevel();
                }
            }

        }

        Instantiate(_affectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        _affectArea.transform.localScale = Vector3.one * _affectRadius * 2f;
    }

    public override void DoEffect()
    {
        base.DoEffect();
        StartCoroutine(AffectProcess());
    }
}
