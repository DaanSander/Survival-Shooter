using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Light))]
[RequireComponent(typeof(AudioSource))]
public class WeaponShooting : MonoBehaviour {

    public LayerMask hitableLayer;

    private LineRenderer lineRenderer;
    private Light muzzleFlash;
    private AudioSource gunShoot;

    private bool canShoot = true;
    private float timer = 0.0f, effectsTimer, maxEffectTime = 0.05f;

    private Animator playerAnimator;
    private int shootHash;

    void Start() {
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
        muzzleFlash = GetComponent<Light>();
        gunShoot = GetComponent<AudioSource>();

        shootHash = Animator.StringToHash("shoot");
    }

    void Update() {
        timer += Time.deltaTime;
        effectsTimer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timer >= 1.0f / EquipmentManager.equippedWeapon.fireRate) {
            InvokeRepeating("shoot", 0f, 1.0f / EquipmentManager.equippedWeapon.fireRate);
            timer = 0.0f;
        }
        else if (Input.GetButtonUp("Fire1"))
            CancelInvoke("shoot");

        if (effectsTimer >= maxEffectTime)
            disableEffects();
    }

    public void disableEffects() {
        playerAnimator.SetBool(shootHash, false);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.enabled = false;
        muzzleFlash.enabled = false;
    }

    public void shoot() {
        RaycastHit hit;

        Debug.Log(transform.parent.gameObject);

        if (!transform.parent.gameObject.activeSelf) return;

        Weapon weapon = EquipmentManager.equippedWeapon;

        effectsTimer = 0.0f;
        muzzleFlash.enabled = true;

        if (playerAnimator.GetBool(shootHash))
            playerAnimator.SetBool(shootHash, false);

        playerAnimator.SetBool(shootHash, true);
        weapon.shootAudio.play(gunShoot);
        
        if (weapon.scatterShot) {

            for (int i = 0; i < weapon.pellets; i++) {
                Debug.Log(i);
                Vector3 offset = transform.parent.parent.up * Random.Range(0.0f, 1.0f);

                offset = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), transform.parent.parent.forward) * offset;

                GameObject lineManager = new GameObject("Pellet " + i);
                LineRenderer line = lineManager.AddComponent<LineRenderer>();

                line.SetWidth(0.01f, 0.01f);
                line.material = lineRenderer.material;
                line.SetPosition(0, transform.position);

                if (Physics.Raycast(transform.position, transform.parent.parent.forward * weapon.spread + offset, out hit, 1000, hitableLayer)) {
                    GameObject enemy = hit.collider.gameObject;

                    line.SetPosition(1, hit.point);

                    EnemyController enemyController = enemy.GetComponent<EnemyController>();
                    if (enemyController != null)
                        enemyController.damage(weapon.damage);
                }
                else
                    line.SetPosition(1, transform.parent.parent.forward * 1000);

                Destroy(lineManager, maxEffectTime);
            }

        } else {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);

            if (Physics.Raycast(transform.position, transform.parent.parent.forward, out hit, 1000, hitableLayer)) {
                GameObject enemy = hit.collider.gameObject;

                lineRenderer.SetPosition(1, hit.point);

                EnemyController enemyController = enemy.GetComponent<EnemyController>();
                if (enemyController == null) return;
                enemyController.damage(weapon.damage);
            }
            else
                lineRenderer.SetPosition(1, transform.parent.parent.forward * 1000);

        }
    }
}
