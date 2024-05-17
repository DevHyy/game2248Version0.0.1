using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    public static DontDestroy instance;//tạo biến static với tên là instance
    private void Awake()
    {
        // DontDestroyOnLoad(gameObject);

        if (instance == null) // kiểm tra xem đã tồn tại chưa,nếu chưa
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // sẽ tạo cái mới,không tự hủy khi chuyển giữa các scene
        }
        else
            Destroy(gameObject); // ngược lại nếu đã tồn tại rồi,thì sẽ hủy gameObject
    }
}
