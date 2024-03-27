'use client';
import DesktopNavigationBar from './DesktopNavigationBar';
import MobileNavigation from './MobileNavigation';
import { useState } from 'react';
import styles from '@/styles/Header/Header.module.css';

const Header = () => {
    const [isMobileNavOpen, setIsMobileNavOpen] = useState<boolean>(false);
    const updateMobileState = () => {
        setIsMobileNavOpen(!isMobileNavOpen);
    };
    return (
        <>
            <header className={styles.primaryHeader}>
                <DesktopNavigationBar
                    updateMobileState={updateMobileState}
                    isMobileActive={isMobileNavOpen}
                />
            </header>
            <MobileNavigation isMobileActive={isMobileNavOpen} />
        </>
    );
};

export default Header;
