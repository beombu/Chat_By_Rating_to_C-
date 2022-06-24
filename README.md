<h1 align="center">Chat_By_Rating_to_C#</h1>

## 프로젝트 소개
해당 프로젝트는 등급별로 더 좋은 정보를 받을 수 있는 등급 채팅방입니다.

클라이언트는 유저와 매니저를 따로 구현하였습니다.

유저들은 미션을 수행하여 레벨을 높여 더 많은 정보를 받아 볼 수 있습니다.

등급은 브론즈, 실버, 골드 순으로 구성하였으면 골드로 갈수록 더 많고 좋은 정보들을 받을 수 있습니다.

**[ 프로토콜 설계 ]**

![image](https://user-images.githubusercontent.com/84673536/175509328-87c4a858-b629-47ea-ae75-b2409d7bd6ef.png)

![image](https://user-images.githubusercontent.com/84673536/175510956-8532101c-b436-44b8-91b1-e16ba6f05719.png)

## 실행 방법

### UserClient
1. 유저의 전체메시지 전송
BR:입력메시지

2. 유저의 1:1메시지 전송
TO:상대클라이언트아이디:입력메시지

3. 하단 레벨업 버튼 클릭 이벤트
(10회 클릭시) 실버 레벨업</br>
(25회 클릭시) 골드 레벨업


### ManagerClient
1. 매니저의 전체메시지 전송
M_BR:입력메시지

2. 매니저의 1:1메시지 전송
M_TO:상대클라이언트아이디:입력메시지

3. 특정 그룹에게 메시지 전송
M_MT:그룹코드(B or S or G):입력메시지

## 실행 화면

<details>
<summary>펼치기</summary>
<div>

> **서버**

![image](https://user-images.githubusercontent.com/84673536/175512134-24f33d05-fb42-48b5-b1a1-4271dc050059.png)

> **유저 클라이언트**

![image](https://user-images.githubusercontent.com/84673536/175512181-80a5492a-f9d4-4098-97b9-8aa1d46fcb7a.png)

> **매니저 클라이언트**

![image](https://user-images.githubusercontent.com/84673536/175512204-ab941845-01ec-4a0c-8864-95d8397bfeac.png)

> **유저(브론즈 -> 실버 승급)**

![image](https://user-images.githubusercontent.com/84673536/175513013-f38359b2-e815-453b-8eb6-7420d97f4c82.png)

> **유저(실버 -> 골드 승급)**

![image](https://user-images.githubusercontent.com/84673536/175512406-3e3263eb-2b92-4205-9c37-8c582c1105c3.png)

> **매니저(골드 그룹 메세지)**

![image](https://user-images.githubusercontent.com/84673536/175512429-fa1366b4-8673-47a4-abd7-b8e1ca04cce7.png)

</div>
</details>

