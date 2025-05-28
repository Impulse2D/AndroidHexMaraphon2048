using UnityEngine;
using YG;

namespace DetectorDeviceType
{
    public class DeviceTypeDetector : MonoBehaviour
    {
        private bool _isMobileDevice;

        public bool IsMobileDevice => _isMobileDevice;

        public void Init()
        {
            if (YandexGame.EnvironmentData.isMobile)
            {
                SetMobileDevice(true);
            }
            else
            {
                SetMobileDevice(false);
            }
        }

        private void SetMobileDevice(bool isMobileDevice)
        {
            _isMobileDevice = isMobileDevice;
        }
    }
}
