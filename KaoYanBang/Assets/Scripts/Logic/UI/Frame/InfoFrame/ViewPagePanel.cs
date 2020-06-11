using liulaoc.UI.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPagePanel : UIPanel
{
    #region 参数设置
    [SerializeField] private float showTimeInterval = 20f;
    [SerializeField] private float moveSpeedTime = 1.0f;
    #endregion
    #region view
    private RectTransform nowPageRec;//显示区域
    private Button lastBtn;
    private Button nextBtn;
    private RectTransform leftPageRec;
    private RectTransform rightPageRec;
    [SerializeField]private Transform group;
    #endregion
    #region model
    private int nowImgIndex = 0;
    private List<ViewPagePrefab> allPages = new List<ViewPagePrefab>();
    private float timer = 0;//用于计时轮播图播放间隔
    private bool isMove = false;
    #endregion
    #region prefabResource
    private GameObject pagePrefab;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        pagePrefab = UIResourceMgr.Instance.Get("PagePrefab");
    }
    protected override void AddListener()
    {
        //显示上一个轮播图
        lastBtn.onClick.AddListener(()=> 
        {
            StartCoroutine(ShowLastPage());
        });
        //显示下一个轮播图
        nextBtn.onClick.AddListener(()=> 
        {
            StartCoroutine(ShowNextPage());
        });
    }
    protected override void BindView()
    {
        nowPageRec = transform.Find("NowPageRec").GetComponent<RectTransform>();
        leftPageRec = transform.Find("LeftPage").GetComponent<RectTransform>();
        rightPageRec = transform.Find("RightPage").GetComponent<RectTransform>();
        lastBtn = transform.Find("LastBtn").GetComponent<Button>();
        nextBtn = transform.Find("NextBtn").GetComponent<Button>();
    }
    private void OnEnable()
    {
        nowImgIndex = 0;
        //获取加载所有轮播图
        GetAllCarouselsMsg msg = new GetAllCarouselsMsg();
        MsgManager.Instance.NetMsgCenter.NetGetAllCarousels(msg,(respond)=> 
        {
            var allCar = JsonHelper.DeserializeObject<List<Carousel>>(respond.data);
            //初始化数据
            if (allCar == null || allCar.Count == 0)
            {
                return;
            }
            for(int i= 0; i<allCar.Count;i++)
            {
                GameObject go = Instantiate(pagePrefab, group);
                var rec = go.GetComponent<RectTransform>();
                var mono = go.GetComponent<ViewPagePrefab>();
                mono.Init(allCar[i]);
                allPages.Add(mono);
                if (i==nowImgIndex)
                {
                    rec.localPosition = nowPageRec.localPosition;
                    rec.sizeDelta = nowPageRec.rect.size;
                }
                else
                {
                    rec.localPosition = leftPageRec.localPosition;
                    rec.sizeDelta = leftPageRec.rect.size;
                }
            }
            //开始播放
            if(allPages.Count >1)
            {
                StartCoroutine(CirclePlayViewPageAsync());
            }
        });
    }
    private void OnDisable()
    {
        //删除所有轮播图
        var n = allPages.Count;
        for(int i=0;i<n;i++)
        {
            Destroy(allPages[i].gameObject);
        }
        allPages.Clear();
        StopAllCoroutines();
    }
    /// <summary>
    /// 播放轮播图
    /// </summary>
    /// <returns></returns>
    private IEnumerator CirclePlayViewPageAsync()
    {
        while(allPages != null)
        {
            timer = 0;
            while (timer <= showTimeInterval)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            //计时结束，移动页面
            yield return StartCoroutine(ShowNextPage());
        }


    }
    private IEnumerator ShowNextPage()
    {
        bool flag = false;
        var nowPage = allPages[nowImgIndex];
        if(nowImgIndex + 1 >= allPages.Count)
        {
            for(int i=0;i<allPages.Count-1;i++)
            {
                allPages[i].GetComponent<RectTransform>().localPosition = leftPageRec.localPosition;
                flag = true;
            }
            nowImgIndex = 0;
        }
        else
        {
            nowImgIndex++;
        }
        var nextPages = allPages[nowImgIndex];
        StartCoroutine(MovePage(nowPage,rightPageRec));
        StartCoroutine(MovePage(nextPages,nowPageRec));
        while (isMove)
        {
            yield return null;
        }
        if (flag)
        {
            nowPage.GetComponent<RectTransform>().localPosition = leftPageRec.localPosition;
        }

    }
    private IEnumerator ShowLastPage()
    {
        bool flag = false;
        var nowPage = allPages[nowImgIndex];
        if(nowImgIndex-1 < 0)
        {
            for(int i=1;i<allPages.Count;i++)
            {
                allPages[i].GetComponent<RectTransform>().localPosition = rightPageRec.localPosition;
                flag = true;
            }
            nowImgIndex = allPages.Count - 1;
        }
        else
        {
            nowImgIndex--;
        }
        var nextPages = allPages[nowImgIndex];
        StartCoroutine(MovePage(nowPage, leftPageRec));
        StartCoroutine(MovePage(nextPages, nowPageRec));
        while (isMove)
        {
            yield return null;
        }
        if (flag)
        {
            nowPage.GetComponent<RectTransform>().localPosition = rightPageRec.localPosition;
        }

    }
    private IEnumerator MovePage(ViewPagePrefab page,RectTransform target)
    {
        isMove = true;
        var timer = 0f;
        var rec = page.GetComponent<RectTransform>();
        Vector2 startPos = rec.localPosition;
        while (timer <= moveSpeedTime)
        {
            timer += Time.deltaTime;
            float t = timer / moveSpeedTime;
            Vector2 pos = Vector2.Lerp(startPos, target.localPosition, t);
            rec.localPosition = pos;
            yield return null;
        }
        isMove = false;
    }
}
