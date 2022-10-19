using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class DragAndDrop : MonoBehaviour
{
    private const float _alpha = 0.5f;
    private Thing _selectedObject;
    private Color _selectedObjectColor;
    private Color _selectedObjectSemitransparentColor;
    private bool _objectPlaced = false;
    private void Start()
    {
        LeanTouch.OnFingerDown += LeanTouch_OnFingerDown;
        LeanTouch.OnFingerUp += LeanTouch_OnFingerUp;
    }

    private void LeanTouch_OnFingerUp(LeanFinger obj)
    {
        if (_selectedObject != null)
        {
            _selectedObject.Place();
            foreach (var thing in _selectedObject.GetComponentsInChildren<Thing>())
            {
                thing.gameObject.layer = 0;
            }
            _selectedObject.gameObject.layer = 0;
            if (_selectedObject.TryGetComponent(out MeshRenderer meshRenderer))
            {
                meshRenderer.material.color = _selectedObjectColor;
            }
            if (_selectedObject.TryGetComponent(out Collider collider))
            {
                collider.enabled = true;
            }
            if (!_objectPlaced)
            {
                _selectedObject.SetLastPoint();
            }
            LeanTouch.OnFingerUpdate -= LeanTouch_OnFingerUpdate;
            _objectPlaced = false;
            _selectedObject = null;
        }

    }

    private void LeanTouch_OnFingerDown(LeanFinger obj)
    {
        Vector2 fingerPosition = obj.ScreenPosition;
        Ray fingerRay = Camera.main.ScreenPointToRay(new Vector3(fingerPosition.x, fingerPosition.y, 0));
        if (Physics.Raycast(fingerRay, out RaycastHit hit))
        {
            if (hit.transform.TryGetComponent(out Thing thing))
            {
                _selectedObject = thing;
                _selectedObject.transform.parent = null;
                foreach (var childThing in _selectedObject.GetComponentsInChildren<Thing>())
                {
                    childThing.gameObject.layer = 3;
                }
                _selectedObject.gameObject.layer = 3;
                if (_selectedObject.TryGetComponent(out MeshRenderer meshRenderer))
                {
                    _selectedObjectColor = meshRenderer.material.color;
                    _selectedObjectSemitransparentColor = _selectedObjectColor - new Color(0,0,0, _alpha);
                }
                if (_selectedObject.TryGetComponent(out Collider collider))
                {
                    collider.enabled = false;
                }
                LeanTouch.OnFingerUpdate += LeanTouch_OnFingerUpdate;
                _objectPlaced = false;
            }
        }
    }

    private void LeanTouch_OnFingerUpdate(LeanFinger obj)
    {
        Vector2 fingerPosition = obj.ScreenPosition;
        Ray fingerRay = Camera.main.ScreenPointToRay(new Vector3(fingerPosition.x, fingerPosition.y, 0));
        if (Physics.Raycast(fingerRay, out RaycastHit hit,100,~(1<<3)))
        {
            if (hit.transform.TryGetComponent(out Surface surface))
            {
                if (_selectedObject.TryToPlaceOnSurface(surface, hit.point))
                {
                    if (_selectedObject.TryGetComponent(out MeshRenderer meshRenderer))
                    {
                        meshRenderer.material.color = _selectedObjectColor;
                    }
                    _objectPlaced = true;
                }
                else
                {
                    Drag(obj);
                }
            }
            else if (hit.transform.TryGetComponent(out Thing thing))
            {
                _selectedObject.TryToPlaceOnThing(thing);
                    if (_selectedObject.TryGetComponent(out MeshRenderer meshRenderer))
                {
                    meshRenderer.material.color = _selectedObjectColor;
                }
                _objectPlaced = true;
            }
            else
            {
                Drag(obj);
            }

        }
        else
        {
            Drag(obj);

        }

    }

    private void Drag(LeanFinger obj)
    {
        if (_selectedObject.TryGetComponent(out MeshRenderer meshRenderer))
        {
            meshRenderer.material.color = _selectedObjectSemitransparentColor;
        }
        Vector3 position = new Vector3(obj.ScreenPosition.x - 12, obj.ScreenPosition.y + 12, Camera.main.WorldToScreenPoint(_selectedObject.transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        _selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
        _objectPlaced = false;

    }
}
