namespace MiFincaVirtual.Common.Models
{
    public class Response
    {
        /// <summary> Indica si fue o no satisfactorio. </summary>
        public bool IsSuccess { get; set; }

        /// <summary> Mensaje en caso de que no sea correcta la respuesta. </summary>
        public string Message { get; set; }

        /// <summary> Datos del resultado.</summary>
        public object Result { get; set; }

    }
}