# mydialog
# 10_04
  1. 타이핑 시스템 작성.
  2. DOTween 배운 기념으로 공부겸 DOText로 작성해 보았으나 기존 방식에 비해 뛰어난거 같지는 않음
  3. 만든김에 바꾸지는 않을 예정
# 10_05
  1. 딕셔너리를 통해 데이터를 관리하고, 아이디를 통해 추출하는 기능 제작
  2. npc의 아이디가 매직넘버를 통해 관리된다는 문제
  3. 모든 데이터가 현재 하나의 딕셔너리에서 관리되는 문제
# 10_06
	1. 외부 csv 데이터를 읽어온 후 npc 이름에 해당하는 id를 매핑시킨 후 데이터를 전달함.
	2. 다만 현재 npc id가 아예 쓸모가 없어진 상황이기 때문에 혹시 미래에도 쓸 일이 없다면 그냥 npc name string으로만 판별하게 바꿀 수도 있음
	3. 문장 데이터는 문장 id를 키로 갖는 딕셔너리로 관리
	4. 매직넘버 문제 어느정도 소멸, 더 이상 모든 문장을 순회하지 않게 됨
	5. 다만 id 관리의 귀찮음 존재 이는 2번에서 설명	
# 10_07
	1. 결국 id 기능은 폐기. name으로 구분하기로 결정
# 10_10
	1. 데이터 파싱 방법을 미연시에 맞는 방법으로 변경.
	2. 컬러를 바꾸는 기능 추가
# 10_11
	1.지금까지 만든 매니저들의 기능을 연결하여 미연시툴 기본 정립