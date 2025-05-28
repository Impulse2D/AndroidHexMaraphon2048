using System.Collections.Generic;
using Hexes;
using UnityEngine;

namespace Cell
{
    public class HexaCell : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private List<int> _idHexes;
        [SerializeField] private int _id;
        [SerializeField] private Hexa _currentHexa;
        [SerializeField] private bool _isBusy;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _selectableMaterial;

        [SerializeField] private GameObject _objectRaycast1;
        [SerializeField] private GameObject _objectRaycast2;
        [SerializeField] private GameObject _objectRaycast3;
        [SerializeField] private GameObject _objectRaycast4;
        [SerializeField] private GameObject _objectRaycast5;
        [SerializeField] private GameObject _objectRaycast6;
        [SerializeField] private int _rangeLiteralRaycasts = 5;

        private MeshRenderer _meshRenderer;
        private bool _isSelectable;

        public bool IsBusy => _isBusy;
        public bool IsSelectable => _isSelectable;

        public Hexa CurrentHexa => _currentHexa;
        public int Id => _id;

        public List<int> IdHexes => _idHexes;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _isSelectable = false;

            _idHexes = new List<int>();

            DisableSelectableMaterial();

            _isBusy = false;
            _currentHexa = null;
        }

        public void TryEnableRaysLateral()
        {
            EnableRayLateral(_objectRaycast1.transform.position, _objectRaycast1.transform.forward, _rangeLiteralRaycasts);
            EnableRayLateral(_objectRaycast2.transform.position, -_objectRaycast2.transform.forward, _rangeLiteralRaycasts);
            EnableRayLateral(_objectRaycast3.transform.position, _objectRaycast3.transform.forward, _rangeLiteralRaycasts);
            EnableRayLateral(_objectRaycast4.transform.position, -_objectRaycast4.transform.forward, _rangeLiteralRaycasts);
            EnableRayLateral(_objectRaycast5.transform.position, _objectRaycast5.transform.forward, _rangeLiteralRaycasts);
            EnableRayLateral(_objectRaycast6.transform.position, -_objectRaycast6.transform.forward, _rangeLiteralRaycasts);
        }

        public void ClearIdHexes()
        {
            _idHexes.Clear();
        }

        public void SetHexa(Hexa hexa)
        {
            if (_isBusy == false)
            {
                _isBusy = true;
            }

            _currentHexa = hexa;
        }

        public void RemoveHexa()
        {
            _currentHexa = null;
        }

        public void EnableIsBusy()
        {
            DisableSelectableMaterial();

            _isBusy = true;
        }

        public void DisableIsBusy()
        {
            _isBusy = false;

            if (_currentHexa != null)
            {
                RemoveHexa();
            }
        }

        public void EnableSelectableMaterial()
        {
            _isSelectable = true;
            _meshRenderer.material = _selectableMaterial;
        }

        public void DisableSelectableMaterial()
        {
            _isSelectable = false;
            _meshRenderer.material = _defaultMaterial;
        }

        private void EnableRayLateral(Vector3 startPosition, Vector3 direction, int rangeLiteralRaycasts)
        {
            RaycastHit hit;

            if (Physics.Raycast(EnableRay(startPosition, direction, rangeLiteralRaycasts), out hit, rangeLiteralRaycasts, _layerMask))
            {
                if (hit.collider.TryGetComponent(out HexaCell hexaCell))
                {
                    if(hexaCell != this && hexaCell.CurrentHexa != null)
                    {
                        _idHexes.Add(hexaCell.CurrentHexa.ID);
                    }
                }
            }
        }

        private Ray EnableRay(Vector3 startPosition, Vector3 direction, int rangeLiteralRaycasts)
        {
            Ray ray = new Ray(startPosition, direction * rangeLiteralRaycasts);
            Debug.DrawRay(startPosition, direction * rangeLiteralRaycasts, Color.yellow);

            return ray;
        }
    }
}
