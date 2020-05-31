import sys
import re
import os.path as path



def main():
    print("RecursiveCalc")
    while True:
        print(">> ", end="")

        txtInput = input()

        if txtInput.lower() == "exit":
            sys.exit()

        lectComandos(txtInput.strip())

#METODO PARA LA LECTURA DE COMANDOS
def lectComandos(texto):
    entrada = texto.lower()

    if entrada.startswith("sort"):
        lista = entrada.replace("sort[","")
        lista = lista.replace("]","")
        lista = lista.replace(" ","")
        patron = "[0-9]*[.]?[0-9]+"
        busqueda = re.findall(patron,lista)
        listaNumeros = [float(x) for x in busqueda]
        if entrada.endswith("asc"):
            sortA(listaNumeros, False)
        elif entrada.endswith("desc"):
            sortD(listaNumeros,False)
        else:
            print("no ingreso un tipo de ordenamiento correcto")

    elif entrada.startswith("sum"):
        lista = entrada.replace("sum[","")
        lista = lista.replace("]","")
        lista = lista.replace(" ","")
        patron = "[0-9]*[.]?[0-9]+"
        busqueda = re.findall(patron, lista)
        listaNumeros = [float(x) for x in busqueda]
        sum(listaNumeros,0,0)
    
    elif entrada.startswith("drawt"):
        lista = entrada.replace(" ","")
        lista = lista.replace("drawtriangle--length=", "")
        if lista.endswith("asc"):
            lista = lista.replace("--type=asc", "")
            triangleA(int(lista),0, "")
        elif lista.endswith("desc"):
            lista = lista.replace("--type=desc", "")
            triangleD(int(lista), "")
        else:
            print("no existe ninguna grafica con esas caracteristicas")

    elif entrada.startswith("drawr"):
        lista = entrada.replace(" ","")
        lista = lista.replace("drawrhombus--size=", "")
        print(lista)
        rombo(int(lista) + 1)
    
    elif entrada.startswith("solve"):
        lista = entrada.replace("solve[", "+0")
        lista = lista.replace("]", "")
        lista = lista.replace(" ", "")
        patron = "[0-9]*[.]?[0-9]+|[a-z]+[(][^)]+[)]"
        listaOperaciones = re.sub(patron,"", lista)
        busqueda = re.findall(patron, lista)
        solve(busqueda,listaOperaciones)
    
    elif entrada.startswith("exec"):
      if "--file=" in entrada:
        lista = entrada.replace(" ","")
        lista = lista.replace("exec --file=","")
        patron = "[0-9]*[a-z]+[0-9]*[.][a-z]+"
        archivo = re.findall(patron,lista)
        if ".calc" in archivo[0]:
          lectura(str(archivo[0]))
        else: 
          print("la extension del archivo es incorrecta")
        
      else:
        print("El comando es incorrecto")

#METODO PARA EL ORDENAMIENTO ASCENDIETE DEL SORT
def sortA(lista, exec):
  size = len(lista)
  if not exec:
    exec = True
    
    for i in range(0,size-1):
      if(lista[i]>lista[i+1]):
        auxiliar = lista[i]
        lista[i] = lista[i+1]
        lista[i+1] = auxiliar
        exec = False
    
    sortA(lista, exec)
  else:
    print(lista)
    

#METODO PARA EL ORDENAMIENTO DESCENDENTE DEL SORT
def sortD(lista, exec):
  size = len(lista)
  if not exec:
    exec = True
    
    for i in range(0,size-1):
      if(lista[i]<lista[i+1]):
        auxiliar = lista[i]
        lista[i] = lista[i+1]
        lista[i+1] = auxiliar
        exec = False
    
    sortD(lista, exec)
  else:
    print(lista)

#METODO PARA EL ORDENAMIENTO DE SUM
def sum(lista, arranque, total):
   size = len(lista)
   tot = total
   if arranque<=size-1:
       total = total + lista[arranque]
       sum(lista, (arranque + 1) , total)
   else:
        print(tot)

#METODO PARA GRAFICAR UN TRIANGULO ASCENDENTE
def triangleA(size, arranque, salida):
    x = salida
    if arranque <= size-1:
        for i in range(0,arranque+1):
            x = x +"*"
        print(x)
        triangleA(size, arranque+1, salida)

#METODO PARA GRAFICAR UN TRIANGULO DESCENDENTE

def triangleD(size, salida):
    x = salida
    if size >= 0:
        for i in range(0,size):
            x = x +"*"
        print(x)
        triangleD(size - 1, "")

#METODO PARA UTILIZAR EL COMANDO SOLVE
def solve(funciones, operadores):
    for x in operadores:
        O1 = actions(str(funciones.pop(0)))
        O2 = actions(str(funciones.pop(0)))
        if x == '+':
            funciones.insert(0, O1 + O2)
        elif x == '-':
            funciones.insert(0, O1 - O2)
        elif x == '/':
            funciones.insert(0,O1/O2)
        elif x == '*':
            funciones.insert(0, O1*O2)
    print(funciones[0])


#METODO PARA GRAFICAR UN ROMBO
def rombo(n):
    if (n-1)%2 != 0:
        print("el tamaño del rombo es incorrecto")
    else:
        for i in range(n+1):
            for j in range(n-i):
             print(" ", end="")
            for k in range(2*i-1):
             print("*",end="")
            print("")
    for i in range(n-1,0,-1):
        for j in range(n-i):
            print(" ", end="")
        for k in range(2*i-1):
            print("*",end="")
        print("")


#Metodo que separa las acciones del solve
def actions(texto):
    entrada = texto.lower()
    if entrada.startswith("exp"):
        lista = entrada.replace("exp(","")
        lista = lista.replace(")","")
        lista = lista.replace(" ","")
        patron = "[0-9]*[.]?[0-9]+"
        numeros = re.findall(patron,lista)
        base = float(str(numeros.pop(0)))
        exponente = float(str(numeros.pop(0)))
        return exp(base,exponente)
 
    elif entrada.startswith("fact"):
        lista = entrada.replace("fact(","")
        lista = lista.replace(")","")
        lista = lista.replace(" ","")
        return fact(int(lista))
    
    elif entrada.startswith("fib"):
        lista = entrada.replace("fib(","")
        lista = lista.replace(")","")
        lista = lista.replace(" ","")
        return fib(int(lista))
    
    elif entrada.startswith("ack"):
        lista = entrada.replace("ack(","")
        lista = lista.replace(")","")
        lista = lista.replace(" ","")
        patron = "[0-9]*[.]?[0-9]+"
        numeros = re.findall(patron,lista)
        numberOne = float(str(numeros.pop(0)))
        numberTwo = float(str(numeros.pop(0)))
        return ack(numberOne,numberTwo)

    else:
        return float((texto))

#metodo para factorial
def fact(num):
    if num == 0 or num == 1:
        return 1
    else:
        return num * fact(num - 1)
#metodo para exponencial
def exp(x,y):
  if y == 0: 
    return 1
  else:
    return x*exp(x,y-1)

#metodo para fibonacci
def fib(num):
    if num == 0 or num == 1:
        return 1
    else:
        return fib(num-1) + fib(num-2)
#metodo para ackerman
def ack(num, num2):
    if num == 0:
        return num2+1
    elif num2 == 0:
        return ack(num-1, 1)
    else:
        return ack(num-1, ack(num, num2-1))
 
#LECTURA DE ARCHIVOS
def lectura(archivo):
  if path.isfile(archivo):
  
    file = open(archivo,"r")
    for line in file.readlines():
      data = line.replace('\n',"")
      lectComandos(str(data))
    file.close()
  else:
    print("El archivo no existe.")

main()
