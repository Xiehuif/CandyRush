using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;//玩家位置
    public float smooth = 0.1f;//平滑因子
    void LateUpdate()
    {
        if (target != null)//保险起见,判空
        {
            if (transform.position != target.position)
            {
                Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10);
                transform.position = Vector3.Lerp(transform.position, targetPosition, smooth);//线性插值
            }
        }
    }
}
