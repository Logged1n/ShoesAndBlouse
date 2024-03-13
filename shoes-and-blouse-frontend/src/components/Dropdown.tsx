import styles from "../styles/Dropdown.module.css";

export default function Dropdown(){
    return(
        <div className={styles.container}>
            <button>Produkty</button>
            <div className={styles.contents}>
                <a href={"#"}>Mężczyzna</a>
                    <div>

                    </div>
                <a href={"#"}>Kobieta</a>
                    <div>

                    </div>
                <a href={"#"}>Dzieci</a>
                    <div>

                    </div>
            </div>
        </div>
    );
}
