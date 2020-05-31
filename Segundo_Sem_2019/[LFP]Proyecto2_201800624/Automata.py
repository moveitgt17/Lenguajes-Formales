class Automata():

    def __init__(self, alphabet, states, initial, finalStates, transitions, stockElements):
        self.alphabet = alphabet
        self.states = states
        self.initial = initial
        self.finalStates = finalStates
        self.transitions = transitions
        self.stockElements = stockElements
    
    def toString(self):
        return "Alfabeto: " + str(self.alphabet) + ", Estados: " + str(self.states) + ", Estado Inicial: "+ str(self.initial) + ", Estados finales: " + str(self.finalStates) + ", Elementos de pila: " + str(self.stockElements) + ", Transiciones: " 