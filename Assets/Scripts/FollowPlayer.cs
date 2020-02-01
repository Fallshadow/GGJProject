
using DG.Tweening;
using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour 
{
    
    public Transform player;//获得角色
    public Vector2 Margin;//相机与角色的相对范围
    public float smoothing;//相机移动的平滑度
    public BoxCollider2D Bounds;//背景的边界
    public float StartCameraZ = -10;
    public float CloseCameraZ = -5;
    public Vector2 StartPos2 = Vector2.zero;
    public float changeZDuration = 1f;
    private Vector3 _min;//边界最大值
    private Vector3 _max;//边界最小值
    private bool isClose = false;
    public bool IsFollowing { get; set; }//用来判断是否跟随

    void Start() {
        player = GameObject.Find("Player").transform;

        IsFollowing = true;//默认为跟随
    }



    void FixedUpdate() {
        _min = Bounds.bounds.min;//初始化边界最小值(边界左下角)
        _max = Bounds.bounds.max;//初始化边界最大值(边界右上角)
        var x = transform.position.x;
        var y = transform.position.y;
        if (IsFollowing) {
            if (Mathf.Abs(x - player.position.x) > Margin.x) {//如果相机与角色的x轴距离超过了最大范围则将x平滑的移动到目标点的x
                x = player.position.x;
            }
            if (Mathf.Abs(y - player.position.y) > Margin.y) {//如果相机与角色的y轴距离超过了最大范围则将x平滑的移动到目标点的y
                y = player.position.y;
            }
        }

        float orthographicSize = GetComponent<Camera>().orthographicSize;//orthographicSize代表相机(或者称为游戏视窗)竖直方向一半的范围大小,且不随屏幕分辨率变化(水平方向会变)
        var cameraHalfWidth = orthographicSize * ((float)Screen.width / Screen.height);//的到视窗水平方向一半的大小
        x = Mathf.Clamp(x, _min.x + cameraHalfWidth, _max.x - cameraHalfWidth);//限定x值
        y = Mathf.Clamp(y, _min.y + orthographicSize, _max.y - orthographicSize);//限定y值
        if(isClose)return;
        transform.position =Vector3.Lerp(transform.position,new Vector3(x,y, transform.position.z),smoothing*Time.fixedDeltaTime);//改变相机的位置
    }
    public void CloseToPlayerPos()
    {
        isClose = true;
        CloseToPos(player.position);//改变相机的位置
    }
    public void AwayToPlayerPos()
    {
        AwayToPos();//改变相机的位置
    }
    public void CloseToPos(Vector2 pos)
    {
        StartPos2 = transform.position;
        transform.DOMove(new Vector3(pos.x,pos.y, CloseCameraZ),changeZDuration);
    }
    public void AwayToPos()
    {
        isClose = false;
        transform.DOMoveZ(StartCameraZ,changeZDuration);
    }
}
