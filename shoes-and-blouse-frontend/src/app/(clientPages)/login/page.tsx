"use client"

import InputLabel from "@/components/InputLabel";
import styles from "@/styles/login.module.css";
import Link from "next/link";
import {useForm} from "react-hook-form";
import axios from "axios";
import {LoginDetails} from "@/app/_types/api_interfaces";

export default function Login() {
    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<LoginDetails>({});
    const onSubmit = async (data: LoginDetails) => {
        try {
           await axios.post("/backendAPI/login?useCookies=true",
               data, {
               headers: {
                   "Content-Type": "application/json",
                   "Accept": "application/json"
               }
           })
               .then(res => console.log(res.data))
        } catch (error) {
            console.error('Błąd walidacji:', (error as Error).message);
        }
    };

    return (
        <div className={styles.content}>
            <form onSubmit={handleSubmit(onSubmit)}
                  noValidate={true}>
                <div className={styles.login}>
                    <h2 style={{fontSize: 50}}>Log In</h2>
                    <InputLabel
                        labelText={"Email"}
                        inputType={"email"}
                        name={"email"}
                        inputId={"email"}
                        placeholderText={"example@fake.com"}
                        required={true}
                        errors={"Wrong email"}
                        register={register}
                    />
                    <InputLabel
                        labelText={"Password"}
                        inputType={"password"}
                        inputId={"password"}
                        name={"password"}
                        placeholderText={"yourpassword123"}
                        required={true}
                        errors={"Wrong password"}
                        register={register}
                    />
                    <button type={'submit'} className={styles.button}>
                        Log In
                    </button>
                </div>
            </form>
                <div className={styles.registerContainer}>
                    <h2 style={{marginLeft: -150, marginBottom: 15, fontSize: 50}}>
                        You don&apos;t have an account?
                    </h2>
                    <button className={styles.button}>
                        <Link className={styles.link} href={"/register"}>
                            Create an Account!
                        </Link>
                    </button>
                </div>

        </div>
)
    ;
}
