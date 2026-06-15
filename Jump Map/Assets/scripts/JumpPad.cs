using UnityEngine;
using StarterAssets;
 
/// <summary>
/// JumpPad - StarterAssets ThirdPersonController 전용 점프대
/// Is Trigger 없이 일반 Collider(Convex MeshCollider or BoxCollider)로 동작합니다.
/// </summary>
public class JumpPad : MonoBehaviour
{
    [Header("── 점프 설정 ──")]
    public float jumpForce = 20f;
    public Vector3 jumpDirection = Vector3.up;
    public bool useWorldDirection = false;
 
    [Header("── 쿨다운 ──")]
    public float cooldown = 0.5f;
 
    [Header("── 시각 / 사운드 효과 ──")]
    public ParticleSystem jumpEffect;
    public AudioClip jumpSound;
    [Range(0f, 1f)] public float soundVolume = 0.8f;
 
    [Header("── 스쿼시 애니메이션 ──")]
    public bool playSquishAnimation = true;
    [Range(0.1f, 1f)] public float squishScaleY = 0.6f;
    public float squishSpeed = 8f;
 
    // ── Private ──────────────────────────────────────────────────
    private AudioSource _audioSource;
    private float       _lastTriggerTime = -999f;
    private Vector3     _originalScale;
    private bool        _isSquishing;
    private float       _squishTimer;
 
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _originalScale = transform.localScale;
    }
 
    private void Update()
    {
        if (playSquishAnimation && _isSquishing)
            UpdateSquish();
    }
 
    // ThirdPersonController의 OnControllerColliderHit에서 호출
    public void TryLaunch(GameObject player)
    {
        if (Time.time - _lastTriggerTime < cooldown) return;
 
        var controller = player.GetComponent<ThirdPersonController>();
        if (controller == null) return;
 
        Vector3 dir = useWorldDirection
            ? jumpDirection.normalized
            : transform.TransformDirection(jumpDirection.normalized);
 
        controller.LaunchFromJumpPad(dir * jumpForce);
 
        PlayEffects();
        _lastTriggerTime = Time.time;
        if (playSquishAnimation) StartSquish();
 
        Debug.Log($"[JumpPad] {player.name} 발사! 방향={dir:F2} 힘={jumpForce}");
    }
 
    private void PlayEffects()
    {
        jumpEffect?.Play();
        if (jumpSound != null)
            _audioSource.PlayOneShot(jumpSound, soundVolume);
    }
 
    private void StartSquish() { _isSquishing = true; _squishTimer = 0f; }
 
    private void UpdateSquish()
    {
        _squishTimer += Time.deltaTime * squishSpeed;
        float t = Mathf.Clamp01(_squishTimer);
        float yScale = Mathf.Lerp(
            _originalScale.y * squishScaleY,
            _originalScale.y,
            Mathf.Sin(t * Mathf.PI * 0.5f));
        transform.localScale = new Vector3(_originalScale.x, yScale, _originalScale.z);
        if (_squishTimer >= 1f) { transform.localScale = _originalScale; _isSquishing = false; }
    }
 
    private void OnDrawGizmosSelected()
    {
        Vector3 dir = useWorldDirection
            ? jumpDirection.normalized
            : transform.TransformDirection(jumpDirection.normalized);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, dir * jumpForce * 0.15f);
        Gizmos.color = new Color(0f, 1f, 1f, 0.4f);
        Gizmos.DrawSphere(transform.position + dir * jumpForce * 0.15f, 0.25f);
    }
}