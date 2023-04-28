## _Querido programador imperial.._

Mi nombre es [Wilhuff Tarkin](https://starwars.fandom.com/es/wiki/Wilhuff_Tarkin) comandante de la flota imperial. 
El mismísimo emperador Palpatin nos ha pedido tener información detallada de las rutas comerciales de la galaxia. Y necesitamos de tus servicios para aportar una solución técnica a este problema.

El caso de uso es el siguiente, necesitamos un servicio web(API) del que podamos obtener información de las rutas comerciales.
- Para tener un mapa de vuelos comerciales se necesita el listado de rutas y la distancia entre ellas.
En este caso necesitamos que la respuesta tenga el siguiente esquema

    ```sh
    //El nombre de los planetas debe estar en un lenguaje entendible por todos los operadores
    //La distancia debe estar en Dias lunares
    [
        {
          "Origin": "Tatooine", 
          "Destination": "Alderaan",
          "Distance":  8743 
        }
    ]
    ```
- Para poder saber el coste en combustible de cada ruta necesitamos un servicio al que le pasemos dos parámetros, “Origin”, “Destination” y obtengamos como respuesta el precio total de la ruta (distancia * precio) y un desglose de los gastos de las rutas, en los que estará el precio por Día lunar, el coste en seguridad de cada planeta y el coste en seguridad de elite.
    - Para obtener el precio `por dia lunar` correcto se debe tener en cuenta el dia la semana en la que se estan pidiendo el precio y el sector o sectores a los que pertenecen los planetas de las rutas. Si los planetas de la ruta pertenecen a dos sectores distintos se debe hacer una media del precio de los dos sectores.
    - Para calcular el precio en seguridad se tendrá que tener en cuenta la influencia rebelde en los dos planetas, si la suma de la influencia de los dos planetas supera el 40% se deberá aplicar una tasa adicional “EliteStromTrooper” que será la diferencia entre la suma del procentaje de los dos planetas y el 40%.
    - La forma correcta de calcular el precio total sería la siguiente: precio base = distancia * precio dia lunar, se suman los porcentajes en defensa y se calcula la tasa de defensa a raiz del precio base. Precio total = precio base + tasas.
    
    ```sh
    //Ejemplo de la petición
    //El nombre de los planetas que se ingresen en la request debe ser en lenguaje entendible para los operadores
    {
      "origin": "Tatooine", 
      "destination": "Aldeeran"
    }
    ```
    En este caso necesitamos que la respuesta tenga el siguiente esquema
    
    ```sh
    {
      "totalAmount": 1235.12, 
      "pricesPerLunarDay": 12.35
      "taxes":{
          "originDefenseCost": 2.12,
          "destinationDefenseCost": 3.56,
          "eliteDefenseCost": 0.00
      }
    }
    ```

El almirante [Motti](https://starwars.fandom.com/es/wiki/Conan_Antonio_Motti/Leyendas) ha dejado las siguientes notas después de su investigación.

> Hola comandante, investigando a fondo de donde obtener la información estática de los planetas que están dentro de las rutas comerciales y la distancia entre ellos, existen los siguientes servicios públicos del sindicato.
> * Planetas: https://otd-exams-data.vueling.app/sindicate/planets.json 
> * Distancias: https://otd-exams-data.vueling.app/sindicate/distances.json 
>
>Estos servicios son del sindicato comercial por lo que cada consulta supone un coste para el imperio, mi recomendación es que esta información se persista de alguna forma para reducir costes.
>Para consultar el precio del combustible tenemos un servicio público del imperio en el que podemos obtener el precio del combustible por `dia lunar`.
> * Precio: https://otd-exams-data.vueling.app/empire/prices2.json
>
>Por último nuestra red de espías ha puesto a nuestra disposición este canal web para obtener el estado de la influencia rebelde en cada uno de los planetas.
> * Rebeldes: https://otd-exams-data.vueling.app/empire/spyreport.json


### El imperio no deja solo a sus programadores, por lo que le daré los siguientes consejos:
* El cambio de años a dias es el terraneo. año = 365 dias.
* Estamos tratando con decimales, intenta no utilizar números con coma flotante.
* Después de cada cálculo, el resultado debe ser redondeado a dos decimales.

### Como programador del imperio esperamos encontrar:
* Los endpoints que suministran info pueden variar por lo que se pide que sean configurables.
* Ver como separas por N capas el proyecto (Servicios distribuidos, capa de aplicación, capa de dominio, ...). 
* Ver como usas SOLID (separación de responsabilidades, Inversión de Dependencias, ...)
* Ver como controlas los errores y como los logueas.
* Ver si usas un correcto naming-convention y consistente.
* Ver como cubres el código con UnitTests.

 **Por favor, una vez finalizada la prueba simplemente debemos crear una petición de incorporación (Pull Request) hacía la rama de master del repositorio, y el último commit contenga en al descripción "Finished". Con esto podemos saber que la prueba ha sido finalizada.**
