import pandas as pd
import matplotlib.pyplot as plt

df = pd.read_csv('D:\projects\Інтелектуальні Інформаційні Системи\LABA 2\Crime_Data_from_2020_to_Present.csv')
plt.style.use('ggplot')
plt.rcParams['figure.figsize'] = (15, 5)


def task1():
 df_res1 = df['Vict Descent'].value_counts()
 print(df_res1)


def task2():
 df_res2 = df[['Crm Cd Desc', 'Vict Sex']].value_counts()
 print(df_res2)


def task3():
 df_res3 = df['Vict Descent'].value_counts()
 print(df_res3)


def task4():
 df_res4 = df['Vict Age','Vict Sex'].value_counts()
 print(df_res4)
 df_res4.plot(kind="bar", figsize=(15, 5), fontsize=10)
 plt.show()


def task5():
 df_res5 = df[['AREA NAME', 'Vict Descent']].value_counts()
 print(df_res5)

task1()
task2()
task3()
task4()
task5()

