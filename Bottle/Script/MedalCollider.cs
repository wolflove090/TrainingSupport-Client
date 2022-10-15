using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalCollider : MonoBehaviour
{
    /* ------------------------------ 
     * 衝突判定
     * ------------------------------ */
	void OnCollisionEnter(Collision other)
	{
        // 衝突の強さ
        const float collisionAmount = 1.5f;

        var mag = other.impulse.magnitude;

        // 衝突量が一定数ならSEを再生
        if(mag >= collisionAmount)
            SoundManager.PlaySe(SoundManager.Se.Coin);
	}
}
