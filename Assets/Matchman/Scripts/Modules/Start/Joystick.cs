// ========================================================
// 描 述：Joystick.cs 
// 作 者：郑贤春 
// 时 间：2017/03/08 21:10:53 
// 版 本：5.4.1f1 
// ========================================================
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

namespace Matchman.Project
{
    [RequireComponent(typeof(RectTransform))]
    public class Joystick : MonoBehaviour,IPointerUpHandler,IPointerDownHandler,IDragHandler
    {
        private enum DirectionLimit
        {
            None = 0,
            Vertical,
            Horizontal
        }

        public Action OnStickStartDrag;
        public Action OnStickEndDrag;
        public Action<Vector2> OnStickDrag;

        [SerializeField]
        private DirectionLimit m_directionLimit = DirectionLimit.None;
        [SerializeField]
        private RectTransform m_handle;
        [SerializeField]
        private Camera m_camera;
        [SerializeField]
        private float m_radius;

        private bool m_isDraging;
        private bool m_isRollback;
        private float m_rollingbackTime;
        private float m_rollbackTime = 0.1f;
        private Vector2 m_axis;
        
        void Start()
        {
            RectTransform rectTransform = transform as RectTransform;
            if (this.m_handle == null)
            {
                GameObject handleObj = new GameObject("Handle");
                handleObj.AddComponent<Image>();
                handleObj.AddComponent<RectTransform>();
                this.m_handle = handleObj.transform as RectTransform;
                this.m_handle.SetParent(transform);
                this.m_handle.localScale = Vector3.one;
                float size = Mathf.Min(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
                this.m_handle.sizeDelta = Vector3.one * size * 0.5f;
            }
            if(this.m_handle.parent != transform)
            {
                this.m_handle.SetParent(transform);
                this.m_handle.localScale = Vector3.one;
            }
            if(this.m_radius == 0)
            {
                this.m_radius = Mathf.Min(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y) * 0.5f;
            }
            this.m_handle.localPosition = Vector3.zero;
        }

        void Update()
        {
            UpdateDrag();
            UpdateRollback();
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateHandlePosition(eventData.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnStartDrag();
            UpdateHandlePosition(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnEndDrag();
        }

        void OnStartDrag()
        {
            this.m_isRollback = false;
            this.m_isDraging = true;
            if (this.OnStickStartDrag != null) OnStickStartDrag();
        }

        void OnEndDrag()
        {
            this.m_isRollback = true;
            this.m_isDraging = false;
            if (this.OnStickEndDrag != null) OnStickEndDrag();
        }

        void UpdateDrag()
        {
            if(this.m_isDraging)
            {
                if (this.OnStickDrag != null) OnStickDrag(this.m_axis);
            }
        }

        void UpdateRollback()
        {
            if(m_isRollback)
            {
                this.m_handle.localPosition = Vector3.Lerp(this.m_handle.localPosition, Vector3.zero, Time.deltaTime / this.m_rollbackTime);
                if(this.m_handle.localPosition.magnitude < 0.1f)
                {
                    this.m_handle.localPosition = Vector3.zero;
                    this.m_isRollback = false;
                }
            }
        }

        void UpdateHandlePosition(Vector2 position)
        {
            Vector3 wp = this.m_camera.ScreenToWorldPoint(position);
            Vector3 lp = transform.InverseTransformPoint(wp);
            if (this.m_directionLimit == DirectionLimit.None)
            {
                lp = new Vector3(lp.x, lp.y).normalized * Mathf.Min(new Vector3(lp.x, lp.y).magnitude, this.m_radius);
            }
            else if(this.m_directionLimit == DirectionLimit.Vertical)
            {
                lp = new Vector3(lp.x, 0f).normalized * Mathf.Min(Math.Abs(lp.x), this.m_radius);
            }
            else if(this.m_directionLimit == DirectionLimit.Horizontal)
            {
                lp = new Vector3(0f, lp.y).normalized * Mathf.Min(Math.Abs(lp.y), this.m_radius);
            }
            this.m_axis = lp / this.m_radius;
            this.m_handle.localPosition = lp;
        }
    }
}

