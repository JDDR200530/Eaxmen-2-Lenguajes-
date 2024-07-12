import { useState } from "react";

export const  App = () =>{
  const [count, setCount] = useState(20);
  return (
    <div> 
      <h1>Mi contador </h1>
      <h2>{count}</h2>
      <button onClick={() => setCount(count +1)} type="button">+1</button>
      <button onClick={()=> setCount(count -1)} type="button">-1</button>
      <p>Hola</p>
    </div>
  );
}

