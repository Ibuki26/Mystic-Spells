using UnityEngine;

public class AreaObjectGroup : MonoBehaviour
{
    [SerializeField] private EnemyPresenter[] enemies;
    [SerializeField] private Item[] items;

    public void ResetObjects()
    {
        if (enemies != null)
        {
            foreach (var enemy in enemies)
                enemy.ResetEnemy();
        }

        if (items != null)
        {
            foreach (var item in items)
                item.gameObject.SetActive(true);
        }
    }
}
