using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool _ignoreNextColl;
    private Rigidbody _rigidbody;
    [SerializeField] private float _impulsForce = 5f;
    private Vector3 _startPosition;
    private int _perfectPass = 0;
    private bool _isSuperSpeedActive;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioJump;
    [SerializeField] private AudioClip _audioDeath;
    [SerializeField] private AudioClip _audioDestroy;
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = transform.position;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (_perfectPass >= 2 && !_isSuperSpeedActive)
        {
            _isSuperSpeedActive = true;
            _rigidbody.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
    } 
    private void OnCollisionEnter(Collision collision)
    {
        if (_ignoreNextColl)
        {
            return;
        }
        if (_isSuperSpeedActive)
        {
            if (!collision.transform.GetComponent<Goal>())
            {
                _audioSource.clip = _audioDestroy;
                _audioSource.Play();
                Destroy(collision.transform.parent.gameObject, 0.3f);

            }
        }
        else
        {
            DeathPart deathPart = collision.gameObject.GetComponent<DeathPart>();
            if (deathPart != null)
            {
                _audioSource.clip = _audioDeath;
                _audioSource.Play();
                deathPart.HitDeathPart();
            }else
            {
                _audioSource.clip = _audioJump;
                _audioSource.Play();
            }
        }
        
        _rigidbody.velocity = Vector3.zero;
        transform.DOShakeScale(0.5f, 0.1f);
        _rigidbody.AddForce(Vector3.up * _impulsForce, ForceMode.Impulse);

        _ignoreNextColl = true;
        Invoke("AllowCollision", 0.2f);

        _perfectPass = 0;
        _isSuperSpeedActive = false;
    }
    private void AllowCollision()
    {
        _ignoreNextColl = false;
    }
    public void RestartBall ()
    {
        
        transform.position = _startPosition;
    }
    public void AddPerfectPast()
    {
        _perfectPass++;
    }
}
