using Airashe.Core.Common.Structs;
using Airashe.Core.Common.Utils;
using UnityEngine;


namespace Airashe.Core.Player
{
    /// <summary>
    /// Компонент камеры, наблюдающая за точкой от третьего лица.
    /// </summary>
    public class ThirdPersonCamera : MonoBehaviour
    {
        /// <summary>
        /// Нормализованный вектор направления, в котором смотрит камера.
        /// </summary>
        public Vector3 LookDirection
        {
            get
            {
                var value = (Vector3.zero - localCameraPosition).normalized;
                value.y = 0;
                return value;
            }
        }

        /// <summary>
        /// Углы камеры. 
        /// X - Угол зенита.
        /// Y - Угол азимута.
        /// </summary>
        public Vector2 CameraAngles
        {
            get => new Vector2(desiredZenithAngle, desiredAzimuthAngle);
            set
            {
                desiredAzimuthAngle.Value = value.y;
                desiredZenithAngle.Value = value.x;
            }
        }
        /// <summary>
        /// Дистанция камеры до точки наблюдения.
        /// </summary>
        public float CameraDistance
        {
            get => desiredSphereRadius.Value;
            set => desiredSphereRadius.Value = value;
        }

        /// <summary>
        /// Локальная позиция камеры.
        /// </summary>
        [SerializeField] private Vector3 localCameraPosition;
        /// <summary>
        /// Позиция камеры, относительно мировых координат.
        /// </summary>
        private Vector3 WorldCameraPosition
        {
            get => sphereCenterWorldPosition + localCameraPosition;
        }
        
        /// <summary>
        /// Позиция центра сферы камеры, в мировых координатах.
        /// </summary>
        public Vector3 sphereCenterWorldPosition;

        /// <summary>
        /// Скорость интерполяции углов камеры.
        /// </summary>
        [Header("Speeds")]
        [SerializeField] private float anglesInterpolationSpeeds;
        /// <summary>
        /// Скорость интерполяции радиуса сферы (в спокойном состоянии).
        /// </summary>
        [SerializeField] private float zoomSpeed;

        /// <summary>
        /// Желаемая дистанция от точки наблюдения.
        /// (Он же радиус сферы)
        /// </summary>
        [Header("Positions")]
        [SerializeField] private BorderedFloat desiredSphereRadius;
        /// <summary>
        /// Целевая дистанция до точки наблюдения (Он же радиус сферы).
        /// </summary>
        [SerializeField] private BorderedFloat actualSphereRadius;
        /// <summary>
        /// Текущая дистанция от точки наблюдения.
        /// (Он же радиус сферы)
        /// </summary>
        private float currentSphereRadius;

        /// <summary>
        /// Желаемый угол азимута.
        /// </summary>
        [SerializeField] private BorderedFloat desiredAzimuthAngle;
        /// <summary>
        /// Текущий угол азимута.
        /// </summary>
        private float currentAzimuthAngle;
        /// <summary>
        /// Желайемый угол зенита.
        /// </summary>
        [SerializeField] private BorderedFloat desiredZenithAngle;
        /// <summary>
        /// Текущий угол зенита.
        /// </summary>
        private float currentZenithAngle;

        /// <summary>
        /// Установить точку, которую просматривает камера.
        /// </summary>
        /// <param name="pointPosition">Координаты точки.</param>
        public void SetObservingPoint(Vector3 pointPosition) => sphereCenterWorldPosition = pointPosition;

        private void Update()
        {
            CheckOutOfBounds();
            InterpolateCurrentSphereRadius();
            InterpolateCurrentCameraAngles();
            CalculateCameraPosition();
            ApplyCurrentFrameData();
        }

        /// <summary>
        /// Высчитать текущий радиус сферы.
        /// </summary>
        private void InterpolateCurrentSphereRadius()
        {
            currentSphereRadius = SmoothUtility.Interpolate(currentSphereRadius, actualSphereRadius, interpolationSpeed: zoomSpeed);
        }

        /// <summary>
        /// Высчитать новые текущие углы камеры.
        /// </summary>
        private void InterpolateCurrentCameraAngles()
        {
            currentAzimuthAngle = SmoothUtility.Interpolate(currentAzimuthAngle, desiredAzimuthAngle, interpolationSpeed: anglesInterpolationSpeeds);
            currentZenithAngle = SmoothUtility.Interpolate(currentZenithAngle, desiredZenithAngle, interpolationSpeed: anglesInterpolationSpeeds);
        }

        /// <summary>
        /// Преобразовать координаты сферической системы в позицию камеры в мире.
        /// </summary>
        private void CalculateCameraPosition()
        {
            localCameraPosition.x = currentSphereRadius * Mathf.Sin(currentAzimuthAngle) * Mathf.Cos(currentZenithAngle);
            localCameraPosition.z = currentSphereRadius * Mathf.Sin(currentAzimuthAngle) * Mathf.Sin(currentZenithAngle);
            localCameraPosition.y = currentSphereRadius * Mathf.Cos(currentAzimuthAngle);
        }

        /// <summary>
        /// Применить изменения к камере на текущий кадр.
        /// </summary>
        private void ApplyCurrentFrameData()
        {
            transform.position = WorldCameraPosition;
            transform.LookAt(sphereCenterWorldPosition);
        }

        /// <summary>
        /// Проверить наличие преград, позади камеры.
        /// </summary>
        private void CheckOutOfBounds()
        {
            var localBehindDirection = localCameraPosition;
            var oobCenterCorrection = Vector3.up * 0.1f;
            if (!Physics.Raycast(sphereCenterWorldPosition + oobCenterCorrection, localBehindDirection, out var hitInfo, desiredSphereRadius))
            {
                actualSphereRadius.Value = desiredSphereRadius;
                return;
            }

            actualSphereRadius.Value = hitInfo.distance - 0.2f;
            currentSphereRadius = actualSphereRadius;
        }

        private void OnDrawGizmos()
        {
            if (!this.enabled)
                return;

            var previousColor = Gizmos.color;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(sphereCenterWorldPosition, desiredSphereRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(sphereCenterWorldPosition, actualSphereRadius);

            var localBehindDirection = localCameraPosition;
            var oobCenterCorrection = Vector3.up * 0.1f;
            Debug.DrawRay(sphereCenterWorldPosition + oobCenterCorrection, localBehindDirection * desiredSphereRadius, Color.yellow);

            Debug.DrawRay(sphereCenterWorldPosition + Vector3.up * 4, LookDirection * 5, Color.cyan);

            Gizmos.color = previousColor;
        }
    }

}
