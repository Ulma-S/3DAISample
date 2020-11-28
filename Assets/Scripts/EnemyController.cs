using UnityEngine;

public class EnemyController : MonoBehaviour{
    [SerializeField] private TargetController[] m_targets;
    [SerializeField] private float m_moveSpeed = 10.0f;
    private int m_currentTargetIdx;
    private const float ThresholdRange = 0.5f;

    private void Start(){
        m_currentTargetIdx = Random.Range(0, m_targets.Length - 1);
    }

    private void Update(){
        //一定距離に近づいたら次のターゲットへ移動
        var distance = (transform.position - m_targets[m_currentTargetIdx].transform.position).magnitude;

        if (distance <= ThresholdRange) {
            m_currentTargetIdx = GetNextTargetRandomly();
        }
        
        MoveToTarget();
    }

    private int GetNextTargetRandomly(){
        if (m_targets.Length <= 1) return 0;
        while (true) {
            var idx = Random.Range(0, m_targets.Length - 1);
            if (idx != m_currentTargetIdx) return idx;
        }
    }

    private void MoveToTarget(){
        var moveDir = (m_targets[m_currentTargetIdx].transform.position - transform.position).normalized;
        
        transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
        transform.Translate(Vector3.forward * m_moveSpeed * Time.deltaTime);
       
    }
}
