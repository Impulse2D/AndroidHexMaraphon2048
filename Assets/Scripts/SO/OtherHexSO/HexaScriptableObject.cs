using UnityEngine;

namespace OtherHexSO
{
    [CreateAssetMenu(fileName = "New Hexa", menuName = "Hexa/Create new Hexa", order = 51)]

    public class HexaScriptableObject : ScriptableObject
    {
        [SerializeField] private string _numberHexa;
        [SerializeField] private Material _material;
        [SerializeField] private float _frotnSize;
        [SerializeField] private int _id;

        public string NumberHexa => _numberHexa;
        public Material Material => _material;
        public float FrontSize => _frotnSize;
        public int ID => _id;
    }
}
