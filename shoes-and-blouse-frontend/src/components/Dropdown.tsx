import {ReactNode} from 'react'
import styles from "../styles/Dropdown.module.css";
import Link from "next/link";

type Props = {
    category: string;
    children?: ReactNode;
}

    const Dropdown: React.FC<Props> = ({category, children}) => {
        return(
            <div className={styles.container}>
                <button>{category}</button>
                {
                    children == null ? null :
                        <div className={styles.contents}>
                            <Link href={"#"}>{children}</Link>
                        </div>
                }

            </div>
        );

}
export default Dropdown;
