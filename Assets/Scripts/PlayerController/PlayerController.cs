
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    public Transform transformCamera; // создаем ссылку на камеру чтобы получить  текущуюпозици
    public Animator anim_Person; //создаем ссылку на компонент аниматор персонажа чтобы включать анимации
    public Rigidbody rb_Person;  // создаем ссылку на компонент Rigidbody чтобы управлять движение персонажа через физику

    public float currentSpeed = 10f; // скорость движения персонажа
    public float jumpForce = 5f; // сила прыжка персонажа
       
    private bool isTerra = false; // флажок для проверки что персонаж стоит на поверхности земли а не на воздухе
    private bool isRunning = false; //проверяем что мы бежим через шифт 

    private Vector3 directionInput;

  

    private void Update()
    {
        CalculateDirectionMove();
        Jumping(); 
    }
    private void CalculateDirectionMove()
    {    
        Vector3 cameraAxisZ = Vector3.ProjectOnPlane(transformCamera.forward, Vector3.up); // получаем направления камеры по оси Z
        Vector3 cameraAxisX = Vector3.ProjectOnPlane(transformCamera.right, Vector3.up); // получаем направления камеры по оси X

        //Input.GetAxis("Horizontal") = это будет равно если мы нажиаем кнопки в право или в лево (D)=1 или (A)=-1  
        //Input.GetAxis("Vertical") = это будет равно если мы нажиаем кнопки в вперед или назад (W)=1 или  (S)=-1  
        //new Vector3 это три оси по X,Y,Z
        float X = Input.GetAxis("Horizontal");
        float Z = Input.GetAxis("Vertical");  

        directionInput = new Vector3(X, 0, Z); // получаем текущее направление движения персонажа

        Vector3 newDirectionMove = (directionInput.z * cameraAxisZ) + (directionInput.x * cameraAxisX);// приводим напрвление к направлению камеры

        // проверяем что персонаж начал движение для того чтобы начать вращать персонажа в сторону направления камеры
        if (newDirectionMove.sqrMagnitude >= 0.2f) 
            transform.rotation = Quaternion.LookRotation(newDirectionMove, Vector3.up); //поворачиваем персонажа в сторону камеры

        MovePerson(newDirectionMove); // включаем движение персонажа по направлению камеры
        PlayPersonAnim(directionInput); // включаем анимацию персонажа с помошью полученых осей
    }
    private void MovePerson(Vector3 directionMove)// входящий параметр в качестве аргумента направление движения
    {
        isRunning = Input.GetKey(KeyCode.LeftShift); //считываем нажатие кнопки Shift

        //если нажали шифт то скорость движения умножаем на 1, без shift -> уменьшаем текущую скорость движен умножив скорость движения на 0,4
        float moveSpeed = currentSpeed * (isRunning ? 1 : 0.4f); // текщая скорость двжиения равна currentSpeed = 10
        rb_Person.MovePosition(rb_Person.position + directionMove * moveSpeed * Time.deltaTime); // включаем движение персон по осям
        
    }
    private void PlayPersonAnim(Vector3 m_Input)
    {
        float animationSpeed = isRunning ? 1 : 0.5f; // без shift -> уменьшаем текущую скорость анимации
        if (m_Input.sqrMagnitude > 0) // получаем значение в магнитуде чтобы проверить что наш персонаж находится в движении
        { 
            // вкл Аним двжиения персонажа по оси X и указываем плавность 0,1 и время
            anim_Person.SetFloat("velocityX", m_Input.x * animationSpeed, 0.1f, Time.deltaTime);
            // вкл Аним двжиения персонажа по оси Z (тоесть Y) и указываем плавность 0,1 и время
            anim_Person.SetFloat("velocityY", m_Input.z * animationSpeed, 0.1f, Time.deltaTime); 
        }
        else
        {
            // выключаем анимацию = 0 это откл Аним и 0,1 это указываем плавность переключения между анимациями  и время Time
            anim_Person.SetFloat("velocityX", 0, 0.1f, Time.deltaTime);
            //выключаем анимацию = 0 это откл Аним и 0,1 указываем плавность переключениямежду анимациями и время Time
            anim_Person.SetFloat("velocityY", 0, 0.1f, Time.deltaTime); 
        }
    }
    private void Jumping()
    {
        bool isJump = Input.GetKeyDown(KeyCode.Space); // считываем нажатия кнопки пробел 
        if (isTerra & isJump) // проверяем что нажата кнока Space и что персонаж находится на поверхнгости земли
        {
            anim_Person.SetBool("isJumping", true); // вкл Аним прыж

            //AddForce это функция физики работает только с компонентом Rigidbody
            rb_Person.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // применяем силу в верх для прыжка,
            isTerra = false; // после прыжка сбрасываем флажок на  false чтобы персонаж не мог прыгать
        }
        else anim_Person.SetBool("isJumping", false); // отключаем анимацию прыжка
    }
    private void OnCollisionEnter(Collision collision) // события столкновений колизии персонажа с поверхностьтю
    {
        if(collision.gameObject.tag == "Terra") //проверка если персонаж на поверхности земли то можно прыгать
            isTerra = true; // устанавливаем флажок на true чтобы можно было прыгать

    }

}
