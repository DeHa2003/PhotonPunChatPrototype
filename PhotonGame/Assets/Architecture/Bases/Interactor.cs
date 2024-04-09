namespace Lessons.Architecture
{
    public abstract class Interactor
    {
        public virtual void OnCreate() { } //����� ��� ���� � ����������� �������
        public virtual void Initialize() { }//����� ��� ���� � ����������� ��������� OnCreate()
        public virtual void OnStart() { }//����� ��� ���� � ����������� ��������� Initialize()
        public virtual void OnDestroy() { }//����� ��� ���� � ����������� ��������� Initialize()
    }
}