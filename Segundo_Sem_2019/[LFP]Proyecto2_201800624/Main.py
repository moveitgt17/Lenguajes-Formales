import os
import sys
import msvcrt
import Grammar as Grammar
from Grammar import Grammar

from graphviz import Digraph

import Transition as Transition
from Transition import Production
from Transition import Transition

import Automata as Automata
from Automata import Automata
start = True
Gramaticas = []#Lista de gramaticas existentes
grammarNames = []#para ver si la gramatica ya existe asi guardar o modificar
productions = {}#sirve para guardar las producciones para una gramatia ya ordenadamenteen biblioteca
OP = [] #Guarda producciones ya ordenadas
automatas = ""
stackColumn=[]
inputColum=[]
transitionColum=[]

def main():
    #DATOS PARA MI LISTA DE PRODUCCIONES DE MI GRAMATICA DE PRUEBA
    productionsTest = ['S>zMNz','M>aMa','N>bNb','N>z','M>z']
    #DATOS PARA MI GRAMATICA DE PRUEBA
    nonTerminalsTest = ["S", "M", "N", "T"]
    terminalsTest = ["a", "b", "z"]
    initialTest = "S"
    productionsTest = makeProductionList(productionsTest)
    saveProductions(productionsTest)
    productionSaved = OP#guardo mis producciones para mi gramtica de prueba
    grammarTest = Grammar("Test", nonTerminalsTest, terminalsTest, productionSaved, initialTest, productionsTest)
    print(grammarTest.toString())
    Gramaticas.append(grammarTest)
    tree(Gramaticas[0])
    #EMPIEZO CON LA CARATULA
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
            os.system("clear")
            menu()
def menu():
    y = True
    while y:
        print("")
        print("*****************MENU PRINCIPAL*********************")
        print("")
        print("1. Crear una gramática o modificar una gramatica")
        print("2. Generar automata de Pila")
        print("3. Visualizar automata")
        print("4. Validar cadena")
        print("5. Regresar a caratula")
        print("6. Salir de aplicación")
        print("")
        x = input("Ingrese la opción que desea seleccionar seguida de enter: ")
        print("")
        if x =="1":
            os.system("clear")
            grammar()
        elif x =="2":
            os.system("clear")
            generateAutomaton()

        elif x =="3":
            os.system("clear")
            if automatas != "":
                graphviz(automatas)
                print("Se ha generado el automata correctamente")
            else:
                print("No se ha generado ningun automata")
        elif x =="4":
            os.system("clear")
            print("validar cadena")
        elif x =="5":
            os.system("clear")
            y = False
            main()
        elif x =="6":
            os.system("clear")
            sys.exit()
        else:
            print("La opcion que elegiste no existe")
def grammar():
    global Gramaticas
    nonTerminals = []
    terminals  = []
    producciones = []#solo guarda una lista de las producciones ingresadas pero no son el conjutno final
    initial = ""
    z = True
    y = input("Ingrese el nombre de la gramatica: ")
    if setGrammars(y):
        while z:
            print("")
            print("1. Ingresar no Terminales")
            print("2. Ingresar Terminales")
            print("3. Ingresar no Terminal inicial")
            print("4. Ingresar Producciones")
            print("5. Borrar Producciones")
            print("6. Generar gramatica")
            print("7. Regresar al menú anterior")
            print("")
            x = input("Ingrese la opcion que desea escoger: ")
            if x=="1":
                nonTerminals.clear()
                os.system("cls")
                i = True
                while i:
                    w = input("Ingrese los no Termianles uno por uno **Para dejar de ingresar ingrese / seguido de enter**: ")
                    if w=="/":
                        i = False
                        print(nonTerminals)
                    else:
                        if w.isupper():
                            nonTerminals.append(w)
                        else:
                            print("Los no terminales deben ser letrar en mayuscula")
            elif x=="2":
                terminals.clear()
                os.system("cls")
                i = True
                while i:
                    w = input("Ingres los terminales uno por uno **Para dejar de ingresar ingrese / seguido de enter**: ")
                    if w=="/":
                        i = False
                        print(terminals)
                    else:
                        if w.islower():
                            terminals.append(w)
                        else:
                            print("Los terminales deben ser letras minusculas")
            elif x=="3":
                initial = ""
                i = input("Ingrese el no Terminal inicial: ")
                if i in nonTerminals:
                    initial = i
                else:
                    print("El estado inicial no se encuetra dentro de los no terminales")
                print(initial)
            elif x=="4":
                producciones.clear()
                productions.clear()
                OP.clear()
                os.system("cls")
                i = True
                while i:
                    w = input("Ingrese las producciones una por una, **para dejar de ingresar producciones presione la tecla / seguida de enter**")
                    if w =="/":
                        print(producciones)
                        i = False
                    else:
                        producciones.append(w)
                producciones = makeProductionList(producciones)
                saveProductions(producciones) 
            elif x=="5":
                h = input("Ingrese la produccion que desea borrar")
                if h in producciones:
                    arrayAux = []
                    for pro in producciones:
                        if h != pro:
                            arrayAux.append(pro)
                        else:
                            print("Se borro la produccion: " +  h)
                    producciones = arrayAux
                    saveProductions(producciones)
                else:
                    print("La produccion " + h + "no se encuentra en las producciones ingresadas")
            elif x=="6":
                grammar = Grammar(y,nonTerminals,terminals,OP,initial,producciones)
                if len(grammar.mistakes)>0:
                    os.system("cls")
                    print("Existen errores en la gramatica, errores: " + grammar.mistakes)
                else:
                    print(grammar.toString())
                    Gramaticas.append(grammar)
            elif x=="7":
                z = False
    else:
        print("La gramatica ya existe por lo que los datos que cambien se modificaran")
        gramatica = giveData(y)
        initialSaved = gramatica.initialNT
        terminalSaved = gramatica.terminals
        nonTerminalsSaved = gramatica.nonTerm
        productionSaved = gramatica.productions
        produccionL = gramatica.producciones
        while z:
            print("")
            print("1. Ingresar no Terminales")
            print("2. Ingresar Terminales")
            print("3. Ingresar no Terminal inicial")
            print("4. Ingresar Producciones")
            print("5. Borrar Producciones")
            print("6. Generar gramatica")
            print("7. Regresar al menú anterior")
            print("")
            x = input("Ingrese la opcion que desea escoger: ")
            if x=="1":
                nonTerminalsSaved.clear()
                os.system("cls")
                i = True
                while i:
                    w = input("Ingrese los no Termianles uno por uno **Para dejar de ingresar ingrese / seguido de enter**: ")
                    if w=="/":
                        i = False
                        print(terminalSaved)
                    else:
                        if w.isupper():
                            nonTerminalsSaved.append(w)
                        else:
                            print("Los no terminales deben ser letras en mayuscula")
            elif x=="2":
                terminalSaved.clear()
                os.system("cls")
                i = True
                while i:
                    w = input("Ingres los terminales uno por uno **Para dejar de ingresar ingrese / seguido de enter**: ")
                    if w=="/":
                        i = False
                        print(terminalSaved)
                    else:
                        if w.islower():
                            terminalSaved.append(w)
                        else:
                            print("Los terminales deben ser letras minusculas")
            elif x=="3":
                initialSaved = ""
                i = input("Ingrese el no Terminal inicial: ")
                if i in nonTerminalsSaved:
                    initialSaved = i
                else:
                    print("El estado inicial no se encuetra dentro de los no terminales")
                print(initialSaved)
            elif x=="4":
                producciones.clear()
                productions.clear()
                OP.clear()
                os.system("cls")
                i = True
                while i:
                    w = input("Ingrese las producciones una por una, **para dejar de ingresar producciones presione la tecla / seguida de enter**")
                    if w =="/":
                        print(producciones)
                        i = False
                    else:
                        producciones.append(w)
                saveProductions(producciones)
                producciones = makeProductionList(producciones)
                productionSaved = OP 
                produccionL = producciones
            elif x=="5":
                os.system("cls")
                h = input("Ingrese la produccion que desea borrar: ")
                if h in produccionL:
                    arrayAux = []
                    for pro in produccionL:
                        if h != pro:
                            arrayAux.append(pro)
                        else:
                            print("Eliminando la produccion: " + h)
                    produccioneL = arrayAux
                    print(produccioneL)
                    saveProductions(produccioneL)
                    productionSaved = OP
                else:
                    print("La produccion "+ h + " no se encuentra en las producciones guardadas de esta gramatica")
            elif x=="6":
                arrayAux = []
                for g in Gramaticas:
                    if y != g.name:
                        arrayAux.append(g)
                gram = Grammar(y, nonTerminalsSaved, terminalSaved,productionSaved, initialSaved,produccionL)
                if len(gram.mistakes)<=0:
                    print("Se ha modifcado la gramatica con nombre " + h)
                    print("")
                    print(gram.toString())
                    arrayAux.append(gram)
                    Gramaticas = arrayAux
                else:
                    print("La datos ingresados para esta gramatica presentan los siguientes errores: " + gram.mistakes)
            elif x=="7":
                z = False           
def setGrammars(x):
    grammarNames.clear()
    for g in Gramaticas:
        grammarNames.append(g.name)
    if x not in grammarNames:
        return True
    else:
        return False
def saveProductions(producciones):
    statesOne = []
    exp = ""
    for p in producciones:
        estadoUno = p.split(">")
        if estadoUno[0] not in statesOne:
            expresions = []
            statesOne.append(estadoUno[0])
            if "|" not in estadoUno[1]:
                exp = estadoUno[1] + "."
                productions[estadoUno[0]] = exp
            else:
                expresiones = estadoUno[1].split("|")
                for e in expresiones:
                    expresions.append(e)
                    productions[estadoUno[0]] = expresions
        elif estadoUno[0] in statesOne:
            arrayAux = []
            if "." not in productions[estadoUno[0]]:
                for e in productions[estadoUno[0]]:
                    arrayAux.append(e)
            elif "." in productions[estadoUno[0]]:
                j = productions[estadoUno[0]]
                j = j.replace(".", "")
                arrayAux.append(j)
            if "|" not in estadoUno[1]:
                arrayAux.append(estadoUno[1])
                productions[estadoUno[0]] = arrayAux
            elif "|" in estadoUno[1]:
                expresiones = estadoUno[1].split("|")
                for e in expresiones:
                    arrayAux.append(e)
                    productions[estadoUno[0]] = arrayAux
    for p in productions:
        product = productions[p]
        if "." in product:
            aux = []
            product = product.replace(".", "")
            aux.append(product)
            produccion = Production(p, aux)
        else:
            produccion = Production(p, product)
        OP.append(produccion)
def giveData(x):
    for g in Gramaticas:
        if g.name == x:
            return g
def makeProductionList(prod):
    arrayAux = []
    for p in prod:
        if "|" not in p:
            arrayAux.append(p)
            print(p)
        if "|" in p:
            arrayAux2 = p.split(">")
            arrayAux3 = arrayAux2[1].split("|")
            for a in arrayAux3:
                exp = arrayAux2[0] + ">" + a
                arrayAux.append(exp)
    print(arrayAux)
    return arrayAux
def generateAutomaton():
    os.system("cls")
    grammarNames.clear()
    for g in Gramaticas:
        grammarNames.append(g.name)
    print(grammarNames)
    x = input("Ingrese el nombre de la gramatica con la que quiere generar el automata")
    if setGrammars(x):
        print("El nombre ingresado no existe")
        os.system("cls")
    else:
        grammar = giveData(x)
        generateStockAutomaton(grammar)
        print("Se ha generado el automata para la gramatica " + x)
def generateStockAutomaton(grammar:Grammar):
    global automatas
    os.system("cls")
    alphabet = grammar.terminals
    initial = "S0"
    final = "S3"  
    states = ["S0", "S1", "S2", "S3"]
    stockElements = []
    transitions = []
    transitionOne = Transition("S0", "S1", "λ", "λ", "#")
    stockElements.append("#")
    transitionTwo = Transition("S1", "S2", "λ", "λ", grammar.initialNT)
    stockElements.append(grammar.initialNT)
    transitionFinal = Transition("S2", "S3", "λ", "#", "λ")
    transitions.append(transitionOne)
    transitions.append(transitionTwo)
    for production in grammar.productions:
        for producccion in production.expresion:
            transition = Transition("S2", "S2", "λ", production.stateOne, producccion)
            stockElements.append(producccion)
            transitions.append(transition)
    for terminal in alphabet:
        transition = Transition("S2", "S2", terminal, terminal, "λ")
        transitions.append(transition)
    transitions.append(transitionFinal)
    aut = Automata(alphabet, states, initial, final, transitions, stockElements)
    print(aut.toString())
    for a in aut.transitions:
        print(a.toStringg())
    automatas =  aut
def graphviz(afd1 : Automata):
    print(afd1.toString())
    for p in afd1.transitions:
        print(p.toStringg())
    f = Digraph(format='png', name='automata')
    f.attr(rankdir="LR", size="8,5")
    
    f.attr('node', shape="doublecircle")
    f.node(afd1.finalStates)

    for estado in afd1.states:
        f.attr('node', shape="circle")
        f.node(estado)

    for trans in afd1.transitions:
        etiqueta = ""
        etiqueta = trans.inputCharacter + "," + trans.readStock + "," + trans.inputStock
        f.edge(trans.origin, trans.destiny, etiqueta)

    f.attr('node', shape="none")
    f.attr("edge", arrowhead="empty", arrowsize="1.5")
    f.edge("", afd1.initial, None)

    f.render('output/'+"automata", view=True)
def tree(grammar: Grammar):
    g = Digraph(name =  "Tree", format="png", node_attr={"shape":"plaintext"})
    x = 0
    aux = {}
    nodoPadre = {}
    y = 0
    for production in grammar.producciones:
        z=x
        stateOne = production.split(">")
        nodos = separate(stateOne[1])
        nodoPadre[z]= stateOne[0]
        if z==0:
            g.node(str(z),nodoPadre[(z)])
        x += 1 
        if y == 1:
            for i in aux:
                for j in nodoPadre:
                    if nodoPadre[j] == aux[i]:
                        z = int(i)
            aux.pop(i)
            nodoPadre.pop(j)
        for hijo in nodos:
            aux[x] = hijo
            g.node(str(x), aux[(x)])
            g.edge(str(z), str(x))
            x +=1
        y = 1
    print(aux)
    print(nodoPadre)
    g.render()
def separate(array):
    newArray = []
    for a in array:
        newArray.append(a)
    print(newArray)
    return newArray

def evaluateStrings(cadena:str, automata:Automata, grammar:Grammar):
    stackColumn.clear()
    inputColum.clear()
    transitionColum.clear()
    x=0
    aux = -1
    for transition in automata.transitions:
        if x ==0:
            push(cadena, inputColum)
            push(transition.toStringg(), transitionColum)
            push("", stackColumn)
        



def pop(pila):
    dato = pila[len(pila)-1]
    pila.remove(pila[len(pila)-1])
    return dato
def push(dato, pila):
    pila.append(dato)

main()