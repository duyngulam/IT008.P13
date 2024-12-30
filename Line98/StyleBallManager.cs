using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Line98
{
    public class StyleBallManager
    {
        private static StyleBallManager _instance;
        private static readonly object _lock = new object();

        // Biến lưu style hiện tại
        public string ResourcePath { get; private set; } = "pack://application:,,,/resources/balls.png";
        public bool isCountingup = true;
        private StyleBallManager() { }

        public static StyleBallManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new StyleBallManager();
                }
                return _instance;
            }
        }

        // Phương thức đổi style
        public void SetStyle(string styleName)
        {
            ResourcePath = $"pack://application:,,,/resources/{styleName}.png";
        }

        public void SetMode(bool type)
        {
            isCountingup = type;
        }
    }
}
