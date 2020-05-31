class Production():
    def __init__(self,stateOne, expresion):
        self.stateOne = stateOne
        self.expresion = expresion

    def toString(self):
        exp = ""
        for E in self.expresion:
            exp = exp + E + "\n | "
        return str(self.stateOne) + "> " + exp

class Transition():
    def __init__(self,origin,destiny, inputCharacter, readStock, inputStock):
        self.origin = origin
        self.destiny = destiny
        self.inputCharacter = inputCharacter
        self.readStock = readStock
        self.inputStock = inputStock

    def toStringg(self):
        return  str(self.origin) + "," + str(self.inputCharacter) + "," + str(self.readStock) + "; " + str(self.destiny) + "," + str(self.inputStock)
        