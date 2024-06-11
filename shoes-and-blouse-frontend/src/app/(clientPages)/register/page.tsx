"use client"

import InputLabel from "@/components/InputLabel";
import styles from "@/styles/register.module.css"
import Link from "next/link";
import {useForm} from "react-hook-form";
import axios from "axios";
import {LoginDetails} from "@/app/_types/api_interfaces";
export default function Register()
{
    const {
        register,
        handleSubmit,
        formState: { errors},
    } = useForm<LoginDetails>({});
    const onSubmit = async (data: LoginDetails) => {
        try {
            await axios.post("/backendAPI/api/v1/Account/register", data, {
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "*/*",
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
                <div className={styles.create}>
                    <h2 style={{fontSize: 50, marginBottom: 20}}>Create an Account</h2>
                    <InputLabel
                        labelText={"Email"}
                        inputType={"email"}
                        inputId={"email"}
                        errors={"Wrong email"}
                        name={"email"}
                        placeholderText={"example@fake.com"}
                        register={register}
                    />
                    <InputLabel
                        labelText={"Password"}
                        inputType={"password"}
                        inputId={"password"}
                        errors={"Wrong password"}
                        name={"password"}
                        placeholderText={"yourpassword123"}
                        register={register}
                    />
                    <button type={'submit'} className={styles.button}>
                            Create
                    </button>
                </div>
            </form>
            <div className={styles.loginContainer}>
                <h2 style={{marginLeft: -150, marginBottom: 40, fontSize: 50}}>
                    You already have an account?
                </h2>
                <button className={styles.button}>
                    <Link className={styles.link} href={"/login"}>
                        Log In!
                    </Link>
                </button>
            </div>
        </div>
    );
}