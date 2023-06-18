import openai
import sys

openai.api_key = "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"

instructions = """
Запомни следующие разделы:
1) Государственные закупки
2) Права
3) Власть
4) Инструкции
5) Миграция
6) Налоги
7) Админресурс
8) Госзакупки
9) Госуслуги
10) Декларации
11) Гражданское участие
12) Конфликт интересов
13) Доступ к информации
14) ГРС:Личные документы
15) Земельная сфера
16) Здравоохранение
17) Образование
18) Армия
Теперь, я задаю вопрос, а ты мне присылаешь ТОЛЬКО НОМЕР РАЗДЕЛА, какой бы я вопрос не задал ты всегда даешь только номер раздела. 

Вопрос: Как получить отстрочку? Ответ: 18.

-"""

import re

def extract_first_number(text):
    match = re.search(r'\d+', text)
    if match:
        return int(match.group())
    else:
        return None
    

response = openai.Completion.create(
  model="text-davinci-003",
  prompt=instructions+sys.argv[1],
)
answer = extract_first_number(response.choices[0].text.strip())
print(answer)