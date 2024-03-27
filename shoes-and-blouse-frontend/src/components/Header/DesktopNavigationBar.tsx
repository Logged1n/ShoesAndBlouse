import styles from '@/styles/Header/DesktopNavigationBar.module.css';
import Link from "next/link";

type updateMobileState = () => void;

interface DesktopNavigationBarProps {
    updateMobileState: updateMobileState;
    isMobileActive: boolean;
}

const DesktopNavigationBar = ({
    updateMobileState,
    isMobileActive
}: DesktopNavigationBarProps) => {

    const handleHamburgClick = () => {
        updateMobileState();
    }

    return (
        <nav className={`${styles.primaryNavigation} ${
            isMobileActive ? styles.mobileActive : ''
        }`}
        id={"primary-navigation"}
        >
            <div className={styles.logoContainer}>
                <Link href={"/"}>
                    Shoes&Blouse
                </Link>
            </div>
            <ul role={"list"} className={styles.navList}>
                <li>
                    <Link href={"#"}>
                        Placeholder 1
                    </Link>
                </li>
                <li>
                    <Link href={"#"}>
                        Placeholder 2
                    </Link>
                </li>
                <li>
                    <Link href={"#"}>
                        Placeholder 1
                    </Link>
                </li>
            </ul>
            <div className={styles.hamburgerWrapper} onClick={handleHamburgClick}>
                <span className={styles.upperHamburger} id={"upperHam"}></span>
                <span className={styles.bottomHamburger} id={"bottomHam"}></span>
            </div>
            <div className={styles.searchWrapper}>
                SearchBar Component
            </div>
        </nav>
    );
}

export default DesktopNavigationBar;
