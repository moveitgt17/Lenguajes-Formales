pila = ["1", "2", "3", "4", "5"]

def pop():
    dato = pila[len(pila)-1]
    pila.remove(pila[len(pila)-1])
    return dato

def push(dato):
    pila.append(dato)
    


            

print(pila)
y = pop()
print(y)
print(pila)
push("Hola")
print(pila)
z = pop()
print(z)
print(pila)

