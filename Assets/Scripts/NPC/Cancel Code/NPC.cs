using UnityEngine;
namespace NPCSpace
{
    public class NPC : MonoBehaviour, ITakeDamage
    {
        public NPCManager NPCManager;
        public void Attack()
        {
            NPCManager.TakeDamage();
        }
    }
}

