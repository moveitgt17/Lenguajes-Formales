class Transition():

    def __init__(self, stateOne, stateTwo, lexeme):
        self.stateOne = stateOne
        self.stateTwo = stateTwo
        self.lexeme = lexeme

    def toString(self):
        return str(self.stateOne) + " " + str(self.stateTwo) + " " + str(self.lexeme)

class Production():
    def __init__(self,stateOne, expresion):
        self.stateOne = stateOne
        self.expresion = expresion

    def toString(self):
        exp = ""
        for E in self.expresion:
            exp = exp + E + "\n | "
        return str(self.stateOne) + "> " + exp

    
 