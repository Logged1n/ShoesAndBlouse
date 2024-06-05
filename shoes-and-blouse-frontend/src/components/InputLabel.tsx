import styles from "@/styles/InputLabel.module.css";

type InputLabelProps = {
    labelText: string;
    inputType: string;
    inputId: string;
    placeholderText?: string;
    register: any;
    errors: any;
    name: string;
    pattern?: string;
    errorMessage?: string
    required?: boolean;
}
const InputLabel = ({ name, register, ...rest }: InputLabelProps) => {
    return (
        <div className={styles.formGroup}>
            <label htmlFor={name}
                   className={styles.blueLabel}>{rest.labelText}</label>
            <input type={rest.inputType}
                   name={rest.inputId}
                   id={rest.inputId}
                   className={styles.formInput}
                   pattern={rest.pattern}
                   aria-invalid={rest.errors[name] ? "true" : "false"}
                   placeholder={rest.placeholderText}
                   {...register(name)}
                   required={rest.required != null ? rest.required : false} />

            {rest.errorMessage !=null ? <span className={styles.errorMessage}>{rest.errorMessage}</span> : null}
        </div>
    );
};

export default InputLabel;