import sys
import msvcrt
import os
import time
import os.path as path

from graphviz import Digraph

import Transition as Transition
from Transition import Transition
from Transition import Production


import AFD as AFD
from AFD import AFD

import Grammar as Grammar
from Grammar import Grammar

import pdfkit
from jinja2 import Environment, FileSystemLoader
start = True
AFDS = []
AFDS_AUX = {}
Gramaticas = []
GramticasAux ={}
def main():
    print("")
    print("*********************************")
    print("Lenguajes formales de programación")
    print("")
    print("Sección B+")
    print("")
    print("Carnet => 201800624")
    print("")
    print("**********************************")
    print()
    while start:
        print(">> ", end="")
        m = str(msvcrt.getch() , 'utf -8')
        if m=="\r":
            menu()
        

def menu():
    y = True
    while y:
        print("**************AFD***************")
        print("")
        print("1. Crear un AFD")
        print("2. Crear una gramática")
        print("3. Evaluar cadenas")
        print("4. Ver Reportes")
        print("5. Cargar archivo de entrada")
        print("6. Salir de la aplicación")
        print("")
        x = input("Ingrese la opción que desea escoger: ")
        print("")
        if x=="1":
            os.system('cls')
            menuAFD()
            y = False
        elif x=="2":
            menuGrammar()
            y = False
        elif x=="3":
            print("Acciones de evaluar cadenas")
        elif x=="4":
            reportes()
            y = False
        elif x=="5":    
            menuArchivos()
        elif x=="6":
            sys.exit()
        else:
            print("La opcion que elegiste no existe")
            y = False

def menuAFD():
      states = []
      alphabet = []
      initialState = ""
      acceptStates = []
      transitions = []
      os.system('cls')
      z = True
      y = input("Ingrese el nombre del AFD a crear: ")
      print(y)
      while z:
          print("")
          print("1. Ingresar estados")
          print("2. Ingresar alfabeto")
          print("3. Estado Inicial")
          print("4. Estados de aceptación")
          print("5. Ingresar transiciones")
          print("6. Ayuda")
          print("7. Generar AFD y gramatica")
          print("8. Regresar")
          print("9. Imprimir todos los AFDS")
          x = input("Ingrese la opción que desea escoger: ")
          if x=="1":
              os.system('cls')
              states.clear()
              i = True
              while i:
                state = input("Ingrese el estado nuevo, ***para dejar de ingresar estados presiones la tecla '/' seguida de enter*** ")
                if state == "/":
                    os.system('cls')
                    print(states)
                    i = False
                else:
                    states.append(state)
          elif x=="2":
              os.system('cls')
              alphabet.clear()
              i = True
              while i:
                symbol = input("Ingrese el símbolo nuevo, **para dejar de ingresar símbolos presiones la tecla '/' seguida de enter** ")
                if symbol == "/":
                    os.system('cls')
                    print(alphabet)
                    i = False
                else:
                    alphabet.append(symbol)
          elif x=="3":
               print("")
               i = input("Ingrese el estado inicial: ")
               initialState = i
               print(i)
          elif x=="4":
              os.system('cls')
              i = True
              acceptStates.clear()
              while i:
                accept = input("Ingrese los estados de aceptacion uno por uno, ***para dejar de ingresar estados de aceptación presiones la tecla '/' seguida de enter***: ")
                if accept == "/":
                    os.system('cls')
                    print(acceptStates)
                    i = False
                else:
                    acceptStates.append(accept)
          elif x=="5":
              transitions.clear()
              print("1. Modo 1")
              print("2. Modo 2")
              i = input("Ingrese el modo para ingresar sus transiciones: ")
              if i=="1":
                os.system('cls')
                i = True
                while i:
                    transition = input("Ingrese las trasiciones *para dejar de ingresar transiciones presiones la tecla '/' seguida de enter* ")
                    if transition == "/":
                        os.system('cls')
                        i = False
                    else:
                        arrayOne =[]
                        arrayOne = transition.split(";")
                        arrayTwo =[]
                        arrayTwo = arrayOne[0].split(",")
                        arrayTwo.append(arrayOne[1])
                        if arrayTwo[2] == "epsilon":
                            print("Error no pueden haber transiciones con epsilon en un AFD solo en un AFN")
                        else:
                            transition = Transition(arrayTwo[0], arrayTwo[1], arrayTwo[2])
                            transitions.append(transition)
                            for t in transitions:
                                print(t.toString())
              elif i =="2":
                  terminals = input("Ingrese los terminales del AFD: ")
                  terminals = terminals.replace("[", "")
                  terminals = terminals.replace("]", "")
                  NT = input("Ingrese los no terminales del AFD: ")
                  NT = NT.replace("[", "")
                  NT = NT.replace("]", "")
                  SD = input("Ingrse los simbolos destino del AFD")
                  SD = SD.replace("[", "")
                  SD = SD.replace("]", "")
                  print(terminals, " ", NT, " ", SD)
                  terminales = terminals.split(",")
                  non_terminals = NT.split(",")
                  i = True
                  while i:
                    i = input("Presione '/' seguido de enter para regresar al menu anterior y completar el AFD")
                    if i=="/":
                        i = False
              else:
                  print("*************La opción que elegiste no existe**************")
          elif x=="6":
            os.system('cls')
            print("")
            print("**********DATOS DEL CURSO********")
            print("")
            print("Curso: Lenguajes Formales de Programación")
            print("Sección: B+")
            print("Auxiliar: Jose Veliz")
            print("Digito: 4")
          elif x=="7":
              afd = AFD(y,alphabet,states,initialState,acceptStates,transitions)
              if len(afd.mistakes) > 0:
                  for m in afd.mistakes:
                      print(m)
              else:
                print(afd.toString())
                AFDS.append(afd)
          elif x=="8":
              z = False
          elif x=="9":
              for afd in AFDS:
                  print(afd.toString())
          else:
              print("")
              print("********La opcion que elegiste no existe************")


def menuGrammar():
    os.system("cls")
    nonTerminals = []
    initial = ""
    terminals = []
    z = True
    produccioens = []
    producciones = {} #Diccionario de producciones
    y = input("Ingrese el nombre de la gramatica  a crear: ")
    while z:        
        print("")
        print("1. Ingresar no Terminales")
        print("2. Ingresar Terminales")
        print("3. Ingresar no Terminal inicial")
        print("4. Ingresar Producciones")
        print("5. Mostrar gramatica transfomada")
        print("6. Ayuda")
        print("7. Generar gramatica y AFD")
        print("8. Regresar al menú anterior")
        print("")
        x = input("Ingrese la opción que desea escoger: ")
        if x=="1":
            nonTerminals.clear()
            os.system("cls")
            i = True
            while i:
                w = input("Ingrese los no terminales de la gramatica uno por uno, **Para dejar de ingresar presione la tecla / seguido de enter** ")
                if w == "/":
                    print(nonTerminals)
                    i = False
                else:
                    nonTerminals.append(w)
        elif x=="2":
            terminals.clear()
            os.system("cls")
            i = True
            while i:
                w = input("Ingrese los terminales de la gramatica uno por uno, **Para dejar de ingresar presione la tecla / seguido de enter** ")
                if w == "/":
                    print(terminals)
                    i = False
                else:
                    terminals.append(w)
        elif x=="3":
            initial = ""
            i = input("Ingrese el no terminal inicial: ")
            initial = i
        elif x=="4":
            produccioens.clear()
            producciones.clear()
            os.system("cls")
            i = True
            while i:
                w = input("Ingrese las producciones una por una, **para dejar de ingresar producciones presione la tecla / seguida de enter**")
                if w =="/":
                    print(produccioens)
                    i = False
                else:
                    produccioens.append(w)            
        elif x=="5":
            for g in Gramaticas:
                if y == g.name:
                    verDetalleG(g)
                else:
                    print("No se ha generado ninguna gramatica con este nombre: "+ y)
        elif x=="6":
            os.system('cls')
            print("")
            print("**********DATOS DEL CURSO********")
            print("")
            print("Curso: Lenguajes Formales de Programación")
            print("Sección: B+")
            print("Auxiliar: Jose Veliz")
            print("Digito: 4")
        elif x=="7":
            nameGrammars = [] #Arreglo para ver las gramaticas creadas y comparar que no existe
            statesOnes = [] #Arreglo para ver el estado donde sale la produccion
            exp ="" #Expresion
            for g in Gramaticas: #recorrre gramaticas creadas
                nameGrammars.append(g) 
            if y not in nameGrammars: #Si no se existe esa gramatica               
                for p in produccioens: # Recorrer todas las producciones asignadas
                    estadoUno = p.split(">")#Separar el estado donde sales
                    if estadoUno[0] not in statesOnes:#si el estado de salida no se repite
                        expresions = []
                        statesOnes.append(estadoUno[0])
                        if "|" not in estadoUno[1]:#ver si la expresion tiene disyuncion
                            exp = estadoUno[1] + "." #si no solo tiene una exprescion
                            producciones[estadoUno[0]] = exp #Guardar la expresion en el diccionario
                            print(producciones)
                        elif "|" in estadoUno[1]:#ver si la exp tiene disyuncion
                            expresiones = estadoUno[1].split("|")
                            for e in expresiones:
                                expresions.append(e)
                                producciones[estadoUno[0]] = expresions
                                print(producciones)
                    elif estadoUno[0] in statesOnes:#si el estado de salida se repite
                        arrayAux = []
                        if "." not in producciones[estadoUno[0]]:
                            for e in producciones[estadoUno[0]]:
                                arrayAux.append(e)
                        elif "." in producciones[estadoUno[0]]:
                            j = producciones[estadoUno[0]]
                            j = j.replace(".", "")
                            arrayAux.append(j)
                        if "|" not in estadoUno[1]:
                            arrayAux.append(estadoUno[1])
                            producciones[estadoUno[0]] = arrayAux
                        elif "|" in estadoUno[1]:
                            expresiones = estadoUno[1].split("|")
                            for e in expresiones:
                                arrayAux.append(e)
                                producciones[estadoUno[0]] = arrayAux
                print(producciones)
                array = []
                for a in producciones:
                    product = producciones[a]
                    p = Production(a, product)
                    array.append(p)
                grammar = Grammar(y,nonTerminals,terminals,array,initial)
                Gramaticas.append(grammar)
                print(grammar.toString())
            elif y in nameGrammars: #Si existe la gramatica
                print("Cambiar datos de esa gramatica")

        elif x=="8":
            z = False

def reportes():
    os.system("cls")
    i = True
    x = "" #NOMBRE
    z = "" #TIPO
    AFDDS = []
    Gramaticass = []
    for a in AFDS:
        AFDDS.append(a.name)
    for g in Gramaticas:
        Gramaticass.append(g.name)

    os.system('cls')
    z = input("Ingrese 'g' seguido de enter para ver una gramatica o 'a' para ver un AFD")
    if z=="g":
        x = input("Ingrese el nombre de la gramatica que desea visualizar: ")
    elif z=="a":
        x = input("Ingrese el nombre del AFD que desea visualizar: ")
    else:
        print("La opcion que elegiste no existe")
        i = False
    while i:
        print("1. Ver detalle")
        print("2. Generar reporte")
        print("3. Ayuda")
        print("4. Regresar")
        y = input("Ingrese la opcion que desea seleccionar")
        if y=="1":
            if z=="g":
                if len(Gramaticas)==0:
                    print("No se ha generado ninguna gramatica")
                else:
                    if x in Gramaticass:
                        for g in Gramaticas:
                            if x == g.name:
                                verDetalleG(g)
                    elif x not in Gramaticass:
                        print("La gramatica ingresada no existe")
            if z=="a":
                if len(AFDS)==0:
                    print("No se ha generado ningun AFD")
                else:
                    if x in AFDDS:
                        for afd in AFDS:
                            if x == afd.name:
                                verDetalleAFD(afd)
                    else:
                        print("El afd ingresado no existe")
        elif y=="2":
            if z=="g":
                if len(Gramaticas)==0:
                    print("No se ha generado ninguna gramatica")
                else:
                    if x in Gramaticass:
                        print("Grmaticas")
                    elif x not in Gramaticas:
                        print("La gramatica ingresada no existe")
            if z=="a":
                if len(AFDS)==0:
                    print("No se ha generado ningun AFD")
                else:
                    if x in AFDDS:
                        for afd in AFDS:
                            if x==afd.name:
                                graphviz(afd)
                                time.sleep(1)
                                pdf(afd)
                    elif x not in AFDS:
                        print("El AFD ingresado no existe")
        elif y=="3":
            os.system('cls')
            print("")
            print("**********DATOS DEL CURSO********")
            print("")
            print("Curso: Lenguajes Formales de Programación")
            print("Sección: B+")
            print("Auxiliar: Jose Veliz")
            print("Digito: 4")
            print("")
        elif y=="4":
            i = False
  
def graphviz(afd1: AFD):
    f = Digraph(format='png', name='prueb')
    f.attr(rankdir="LR", size="8,5")
    
    for est in afd1.acceptStates:
        f.attr('node', shape="doublecircle")
        f.node(est)

    for estado in afd1.states:
        f.attr('node', shape="circle")
        f.node(estado)

    for trans in afd1.transitions:
        f.edge(trans.stateOne, trans.stateTwo, label=trans.lexeme)

    f.attr('node', shape="none")
    f.attr("edge", arrowhead="empty", arrowsize="1.5")
    f.edge("", afd1.initialState, None)

    f.render()

def pdf(afd: AFD):
    env=Environment(loader=FileSystemLoader("templates"))
    template = env.get_template("reporte.html")
    d = {"name":afd.name, "init": afd.initialState, "as":afd.acceptStates, "es":afd.states, "al":afd.alphabet}
    convertAFD(afd)
    #Pasar los elementos del diccionario de la gramatica convertida al dicconario d
    for g in GramticasAux:
        d[g] = GramticasAux[g]
    productions = "<p>"
    for a in d.keys():
        helpp =""
        print(a)
        if a!= "name" and a!= "init" and a!="as" and a!="es" and a!="al" and a!="startt" and a!="term" and a!="nTerm" :
            for xx in d[a]:
                helpp = helpp + xx
            productions = productions + a + "->" + helpp +"</p>"
    d["ppp"] = productions
    print(d)
    html = template.render(d)
    f=open('nuevoR.html', 'w')
    f.write(html)
    f.close()
    pdfkit.from_file('nuevoR.html', 'nuevoR.pdf')

def convertAFD(afd:AFD):
    GramticasAux.clear()
    GramticasAux["startt"] = "S"
    nonTerm = []
    aux = []
    i = False
    h = True
    x = 0
    #ESTO ES PARA VALIDAR QUE EL ESTADO INICIAL ENTRA ARCO Y SEA NO TERMINAL
    for a in afd.transitions:
        aux.append(a.stateOne)
        if afd.initialState == a.stateTwo:
            x+=1
    if x>=1 :
        nonTerm.append(afd.initialState)
        i = True
    #VALIDAR QUE ESTADO ACEPTACION TENGA ARCO 
    for a in afd.acceptStates:
        if a in aux:
            nonTerm.append(a)
    #AGREGAR ESTADOS RESTANTES A NO TERMINALES
    for a in afd.states:
        if a not in afd.acceptStates and a != afd.initialState:
            nonTerm.append(a)
    #AGREGAR TERMINALES Y NO TERMINALES AL DICCIONARIO
    GramticasAux["term"] = afd.alphabet
    h = []
    for n in nonTerm:
        if n not in h:
            h.append(n)
    GramticasAux["nTerm"] = h
    #
    #CREAR PRODUCCIONES A PARTIR DE TRANSICIONES
    for a in afd.transitions:
            if a.stateOne == afd.initialState:
                if i:
                    if "S" in GramticasAux.keys():
                        GramticasAux["S"] = GramticasAux["S"] + "|" + a.lexeme + a.stateTwo
                        if a.stateTwo in afd.acceptStates:
                             GramticasAux["S"] = GramticasAux["S"] + "|" + a.lexeme
                    elif "S" not in GramticasAux.keys():
                        GramticasAux["S"] = a.lexeme + a.stateTwo
                        if a.stateTwo in afd.acceptStates:
                             GramticasAux["S"] = GramticasAux["S"] + "|" + a.lexeme
                    GramticasAux[afd.initialState] = GramticasAux["S"]
                else:
                    if "S" in GramticasAux.keys():
                        GramticasAux["S"] = GramticasAux["S"] + "|" + a.lexeme + a.stateTwo
                        if a.stateTwo in afd.acceptStates:
                             GramticasAux["S"] = GramticasAux["S"] + "|" + a.lexeme
                    elif "S" not in GramticasAux.keys():
                        GramticasAux["S"] = a.lexeme + a.stateTwo
                        if a.stateTwo in afd.acceptStates:
                             GramticasAux["S"] = GramticasAux["S"] + "|" + a.lexeme
            else:
                if a.stateOne in GramticasAux.keys():
                    GramticasAux[a.stateOne] = GramticasAux[a.stateOne] + "|" + a.lexeme + a.stateTwo
                    if a.stateTwo in afd.acceptStates:
                        GramticasAux[a.stateOne] = GramticasAux[a.stateOne] + "|" + a.lexeme
                else:
                    GramticasAux[a.stateOne] = a.lexeme + a.stateTwo
                    if a.stateTwo in afd.acceptStates:
                        GramticasAux[a.stateOne] = GramticasAux[a.stateOne] + "|" + a.lexeme
    
    if afd.initialState in afd.acceptStates:
        if "S" in GramticasAux.keys():
            GramticasAux["S"] = GramticasAux["S"] + "|" + "epsilon"
        else:
            GramticasAux["S"] = "epsilon"
    
def verDetalleAFD(afd:AFD):
    os.system("cls")
    print("")
    print("*********************************************")
    x = ""
    for i in afd.alphabet:
        x = x + i + ", "
    e = ""
    for i in afd.states:
        e = e +i +", "
    a = ""
    for i in afd.acceptStates:
        a = a + i +", "
    print("AFD: " + afd.name)
    print("")
    print("Alfabeto: "+ x)
    print("Estados:  "+ e)
    print("Estado Inicial: "+ afd.initialState)
    print("Estados Aceptaciòn: "+ a)
    print("Transiciones: ")
    for t in afd.transitions:
        print(t.stateOne + "," + t.stateTwo + ";" + t.lexeme)    
    print("********************************************************")
    print("")

def verDetalleG(g: Grammar):
    os.system("cls")
    print("")
    print("*********************************************************")
    print("Gramatica: " + g.name)
    print("")
    x =""
    for i in g.nonTerm:
        x = x + i +", "
    print("No terminales: " + x)
    z =""
    for i in g.terminals:
        z = z + i + ", "
    print("Terminales: " + z)
    print("Inicio: " + g.initialNT)
    print("Producciones: ")
    for p in g.productions:
        y = ""
        for i in p.expresion:
            y = y + i + "|"
        print(p.stateOne + "->" + y)
    print("************************************************************")
    print("")

def lectura(archivo, b):
    initialState = ""
    alpha = []
    states = []
    acceptStates = []
    transitions = []

    if path.isfile(archivo):
        file = open(archivo,"r")
        for line in file.readlines():
            data = line.replace('\n',"")
            if b =="a":
                array1 = data.split(";")
                array2 = array1[0].split(",")
                array3 = array1[1].split(",")

                if initialState=="":
                    initialState = array2[0]
                if array2[2] not in alpha:
                    alpha.append(array2[2])
                if array2[0] not in states:
                    states.append(array2[0])
                if array2[1] not in states:
                    states.append(array2[1])
                if array3[0] == "true":
                    if array2[0] not in acceptStates:
                        acceptStates.append(array2[0])
                if array3[1] == "true   ":
                    if array2[1] not in acceptStates:
                        acceptStates.append(array2[1])
                
                t = Transition(array2[0], array2[1], array2[2])
                transitions.append(t)

                afd = AFD(archivo,alpha,states,initialState,acceptStates,transitions)
                print(afd.toString())
                if len(afd.mistakes)==0:
                    AFDS.append(afd)
                else:
                    print(afd.mistakes)
            if b=="g":
                array1 = data.split(">")
        file.close()
    else:
        print("El archivo no existe.")

def menuArchivos():
    os.system("cls")
    i = True
    while i:
        print("")
        print("1. Cargar un AFD")
        print("2. Cargar Gramatica")
        print("3. Regresar")
        print("")
        x = input("Ingrese la opcion que desea seleccionar: ")
        if x=="1":
            y = input("Ingrese el nombre del archivo que desea cargar: ")
            if ".afd" not in y:
                print("La extensiòn del archivo es incorrecta")
            else:
                lectura(y, "a")
        elif x=="2":
            y = input("Ingrese el nombre del archivo que desea cargar: ")
            if ".grm" not in y:
                print("La extennsiòn del archivo es incorrecta")
            else:
                lectura(y, "g")
        elif x=="3": 
            i = False
        else:
            print("La opcion que elegiste no existe")

main()