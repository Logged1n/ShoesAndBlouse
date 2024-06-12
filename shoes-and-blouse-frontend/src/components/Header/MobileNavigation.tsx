import Link from "next/link";

import styles from "@/styles/Header/MobileNavigation.module.css";

type MobileNavigationProps = {
    isMobileActive: boolean;
}

const MobileNavigation = ({ isMobileActive } : MobileNavigationProps) => {
    return (
        <>
            <nav className={`${styles.mobileNavigation} ${
                isMobileActive ? styles.active : ''}`}>
                <div className={styles.searchWrapper}>
                    SearchBar component
                </div>
                <ul role={"list"} className={styles.navList}>
                    <li>
                        <Link href={"#"}>Shoes</Link>
                    </li>
                    <li>
                        <Link href={"#"}>Cloths</Link>
                    </li>
                    <li>
                        <Link href={"#"}>Others</Link>
                    </li>
                </ul>
            </nav>
        </>
    );
}

export default MobileNavigation;
