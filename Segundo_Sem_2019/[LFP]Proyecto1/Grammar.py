import Transition as Transition
from Transition import Production
import sys
import os.path as path

class Grammar():

    def __init__(self, name, NT,terminals, productions, initialNT):
        self.name = name
        self.initialNT = initialNT
        self.terminals = terminals
        self.nonTerm = NT
        self.productions = productions
        self.mistakes = []
        self.nonTerminalsMistakes()
        self.terminalsMistakes()
        self.terminalsVSnonTerm()
        self.initialNT_Mistake()
        self.productionsMistake()
    
    def nonTerminalsMistakes(self):
        nonTerminalsTwo = []
        for NT in self.nonTerm:
            if NT not in nonTerminalsTwo:
                nonTerminalsTwo.append(NT)
            else:
                self.mistakes.append("Error existen no terminales repetidos con: "+ NT)
    
    def terminalsMistakes(self):
        terminalsTwo = []
        for T in self.terminals:
            if T not in terminalsTwo:
                terminalsTwo.append(T)
            else:
                self.mistakes.append("Error existen terminales repetidos con: " + T)
    
    def terminalsVSnonTerm(self):
        for mistake in self.terminals:
            if mistake in self.nonTerm:
                self.mistakes.append("Error existe un terminal y un no terminal igual con: "+ mistake)
    
    def initialNT_Mistake(self):
        if self.initialNT not in self.nonTerm:
            self.mistakes.append("Error el no terminal inicial: " +  self.initialNT+  " no se encuentra en los terminales establecidos")

    def productionsMistake(self):
        productionsTwo =[]
        for P in self.productions:
            if P not in productionsTwo:
                productionsTwo.append(P)
            else:
                self.mistakes.append("Existen producciones repetidas con: " + P.stateOne)
    
    def toString(self):
        if len(self.mistakes) < 1:
            NT = "No terminales: "
            for nonT in self.nonTerm:
                NT = NT + nonT + ", "
            T = "Terminales: "
            for term in self.terminals:
                T = T + term + ", "
            P = "Producciones: "
            for prod in self.productions:
                P = P + prod.toString() + "\n"
            return self.name + NT + T + "Estado Inicial: " + self.initialNT + " " + P
        else:
            m = "Errores: \n"
            for mm in self.mistakes:
                m = m + mm + "\n "
            return m


#LECTURA DE ARCHIVOS
def lectura(archivo):
  if path.isfile(archivo):
    file = open(archivo,"r")
    for line in file.readlines():
      data = line.replace('\n',"")
      #print(data)
      #print("*")
      print(line)
    file.close()
  else:
    print("El archivo no existe.")

#lectura("entrada.calc")

