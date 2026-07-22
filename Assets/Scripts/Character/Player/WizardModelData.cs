using UnityEngine;

//プレイヤーのステータスのコピー用データ
//ScriptableObjectでインスペクターからこれを登録して使う
[CreateAssetMenu(menuName = "ScriptableObjects/Player")]
public class WizardModelData : ScriptableObject
{
    [SerializeField] private int _hitPoint; //体力
    [SerializeField] private int _strength; //攻撃力
    [SerializeField] private int _defense; //防御力
    [SerializeField] private float _speed; //移動の速さ
    [SerializeField] private float _jumpVelocity; //ジャンプの速さ

    public int HitPoint => _hitPoint;
    public int Strength => _strength;
    public int Defense => _defense;
    public float Speed => _speed;
    public float JumpVelocity => _jumpVelocity;
}
