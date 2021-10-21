using UnityEngine;

namespace coffee.Dialogue
{
    /// <summary>
    /// 2021.1021
    /// ��ܨt��
    /// NPC ��ܤT���q�G�������ȫe�B���ȶi�椤�B��������
    /// </summary>
    [CreateAssetMenu(menuName = "coffee/Data Dialogue", fileName = "NPC Data Dialogue")]
    public class DataDialogue : ScriptableObject
    {
        [Header("���ȫe��ܤ��e"), TextArea(2, 7)]
        public string[] beforeMission;
        [Header("���ȶi�椤��ܤ��e"), TextArea(2, 7)]
        public string[] missionning;
        [Header("���ȧ�����ܤ��e"), TextArea(2, 7)]
        public string[] afterMission;
        [Header("���ȻݨD�ƶq"), Range(0, 100)]
        public int countNeed;
        [Header("NPC ���Ȫ��A")]
        public StateNPCMission stateNPCMission = StateNPCMission.BeforeMission;
    }
}