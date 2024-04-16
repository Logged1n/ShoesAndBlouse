"use client"

import axios from "axios";

export default function Dummy() {
    const makeApiCall = async (): Promise<string> => {
         const data = await axios.get(`backendAPI/v1/Product/GetById/1`);
         console.log(data);
        return JSON.stringify(data);
    }

    return (
        <div>
            <button onClick={makeApiCall}>Call</button>
        </div>
    );
}