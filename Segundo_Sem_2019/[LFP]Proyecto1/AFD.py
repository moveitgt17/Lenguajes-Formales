import Transition 
from Transition import Transition
class AFD():

    def __init__(self,name,alphabet,states,initialState,acceptStates,transitions):
        self.name = name
        self.alphabet = alphabet
        self.states = states
        self.initialState = initialState
        self.acceptStates = acceptStates
        self.transitions = transitions
        self.mistakes = []
        self.alphabetMistakes()
        self.statesMistakes()
        self.alphabetStates()
        self.initialStateMistake()
        self.acceptStatesMistakes()


    def statesMistakes(self):
        statesTwo = []
        for state in self.states:
            if state not in statesTwo:
                statesTwo.append(state)
            else:
                self.mistakes.append("Error existen estados repetidos con: " + state)

    def alphabetMistakes(self):
        alpahbetTwo = []
        for lexem in self.alphabet:
            if lexem not in alpahbetTwo:
                alpahbetTwo.append(lexem)
            else:
                self.mistakes.append("Error existen estados repetidos con: " + lexem)

    def alphabetStates(self):
        for mistake in self.alphabet:
           if mistake in self.states:
               self.mistakes.append("Error existe un estado igual a un simbolo con: " + mistake)
        
    def initialStateMistake(self):
        if self.initialState not in self.states:
            self.mistakes.append("Error el estado inicial: '"+ self.initialState + "'  no se encuentra en los estados establecidos")

    def acceptStatesMistakes(self):
        for accept in self.acceptStates:
            if accept not in self.states:
                self.mistakes.append("Error el estado de aceptación: '" + accept + "' no se encuentra en los estados establecidos")

    def acceptInitialState(self):
        if self.initialState in self.acceptStates:
            self.mistakes.append("Error el estado inicial: '" + self.initialState +"' no puede ser un estado de aceptación")

    def toString(self):
        abc = ""
        for a in self.alphabet:
            abc = abc + a + ", "
        estados = ""
        for e in self.states:
            estados = estados + e + ", "
        EA = ""
        for ea in self.acceptStates:
            EA = EA +ea +", "
        transicion = ""
        for t in self.transitions:
            transicion = transicion + t.toString() + "* "
        return "Nombre: " + str(self.name) + "  Estado Inicial:" + str(self.initialState) + "Alfabeto: " + abc + "Estados: " + estados + "Estados de aceptación: " + EA + "Transiciones: " + transicion

    
    
        




