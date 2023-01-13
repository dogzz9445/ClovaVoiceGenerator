# ClovaVoiceGenerator - WPF

WPF UI Voice Generator for CLOVA Voice


![main page](Resources/images/app_main.png?raw=true "메인 페이지")
이미지이미지

## Get Started
> Auth 추가하기
``` json
// Resources/auth.json
{
  "client_id": "test",
  "client_secret": "secret"
}
```


## TODO:

- 파일위치 열기 기능
- 음성합성 디폴트 세팅 만들기
- 음성 합성 설정 옆에 음성합성 디폴트 세팅하는 버튼 만들기

- 프로그램 처음 시작 시에 초기 설정 창
- 초기 설정 창에서 Auth 없을 시 Auth 설정하는 기능
- 초기 설정 창에서 저장할 파일 위치 지정 기능
- 프리셋 기능 추가
- ClovaSettings 속성으로 ClovaVoicePreset 추가
- AppSettings 에 있는 설정 내용 ClovaVoiceSettings 로 이동 및 파일 분리
- HomeViewModel 에 있는 내용 ClovaVoiceController 로 이동
- 설정 버튼으로 창 열어서 설정 창 띄우기
- 설정창에서 Auth 설정 변경 기능
- 설정창에서 저장할 파일 위치 지정 기능
- 보이스 샘플 추가
- 클로바보이스 Speaker 리스트 최신으로 업데이트
- 우측 레이아웃에 저장되어있는 파일 위치에서 오디오 리스트 받아서 플레이 가능하게하기
- 가운데 레이아웃(음성합성) 밑에서 합성한 오디오 재생 기능 추가
- 가운데 레이아웃(음성합성) 밑에서 합성한 오디오 삭제 기능 추가
- 음성 합성 버튼 누를 시 progress UX 추가
