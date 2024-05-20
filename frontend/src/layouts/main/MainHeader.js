import { useLocation } from 'react-router-dom';
// @mui
import { styled, useTheme } from '@mui/material/styles';
import { Box, Button, AppBar, Toolbar, Container, Divider, IconButton, Typography, Stack, InputBase, Badge } from '@mui/material';
import LocationOnOutlinedIcon from '@mui/icons-material/LocationOnOutlined';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import SearchIcon from '@mui/icons-material/Search';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import PhoneInTalkOutlinedIcon from '@mui/icons-material/PhoneInTalkOutlined';

// hooks
import useOffSetTop from '../../hooks/useOffSetTop';
import useResponsive from '../../hooks/useResponsive';
// utils
import cssStyles from '../../utils/cssStyles';
// config
import { HEADER } from '../../config';
// components
import Logo from '../../components/Logo';
import Label from '../../components/Label';
//
import MenuDesktop from './MenuDesktop';
import MenuMobile from './MenuMobile';
import navConfig from './MenuConfig';
import SearchBar from './SearchBar';

// ----------------------------------------------------------------------

const ToolbarStyle = styled(Toolbar)(({ theme }) => ({
  height: HEADER.MOBILE_HEIGHT,
  transition: theme.transitions.create(['height', 'background-color'], {
    easing: theme.transitions.easing.easeInOut,
    duration: theme.transitions.duration.shorter,
  }),
  [theme.breakpoints.up('md')]: {
    height: HEADER.MAIN_DESKTOP_HEIGHT,
  },
}));

const ToolbarShadowStyle = styled('div')(({ theme }) => ({
  left: 0,
  right: 0,
  bottom: 0,
  height: 24,
  zIndex: -1,
  margin: 'auto',
  borderRadius: '50%',
  position: 'absolute',
  width: `calc(100% - 48px)`,
  boxShadow: theme.customShadows.z8,
}));
const TextStyle = styled(Typography)({
  color: '#797979',
  fontWeight: 400 // Ensure consistent font weight
});

const IconButtonStyle = styled(IconButton)({
  color: '#797979',
});

const ButtonStyle = styled(Button)({
  color: '#797979',
  textTransform: 'none',
  marginRight: '8px', // Adjust as needed
  fontWeight: 400 // Match the Typography font weight
});
// ----------------------------------------------------------------------

export default function MainHeader() {
  const isOffset = useOffSetTop(HEADER.MAIN_DESKTOP_HEIGHT);

  const theme = useTheme();

  const { pathname } = useLocation();

  const isDesktop = useResponsive('up', 'md');

  const isHome = pathname === '/';

  return (
    <AppBar position="fixed" sx={{ boxShadow: 0, backgroundColor: 'white' }}>

      <Container sx={{
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'space-between',
      }}>
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          <IconButtonStyle aria-label="location">
            <LocationOnOutlinedIcon />
          </IconButtonStyle>
          <TextStyle variant="body2" sx={{ flexGrow: 1 }}>
            FPT University – E2a-7, D1 Street, Long Thanh My, Thu Duc City, HCM City.
          </TextStyle>
        </Box>
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          <Box sx={{ display: 'flex', alignItems: 'center' }}>
            <ButtonStyle endIcon={<KeyboardArrowDownIcon />}>
              Eng
            </ButtonStyle>
            <ButtonStyle endIcon={<KeyboardArrowDownIcon />}>
              USD
            </ButtonStyle>
          </Box>
          <Divider orientation="vertical" variant='middle' flexItem sx={{ borderColor: '#797979' }} />

          <Box sx={{ display: 'flex', alignItems: 'center' }}>
            <ButtonStyle sx={{ marginRight: 0.5 }}>
              Sign In
            </ButtonStyle>
            <Divider orientation="vertical" variant='middle' flexItem />
            <ButtonStyle>
              Sign Up
            </ButtonStyle>
          </Box>
        </Box>
      </Container>


      <Divider />

      <Container
        sx={{
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'space-between',
          paddingBlock: 2
        }}
      >
        <Box display='flex' alignItems='center'>
          <IconButton edge="start" color="inherit" aria-label="open drawer">
            <img src="/logo/logoexe2.png" alt="Unimarket" style={{ height: 32 }} />
          </IconButton>
          <Typography variant="h6" color="inherit" noWrap sx={{ flexGrow: 1 }}>
            Uni
            <Typography component="span" variant="h6" sx={{ color: 'primary.main' }}>
              &nbsp;market
            </Typography>
          </Typography>
        </Box>
        <SearchBar />
        <Box display='flex'>
          <IconButton aria-label="show favorites" color="inherit">
            <FavoriteBorderIcon />
          </IconButton>
          <Divider orientation="vertical" variant="middle" flexItem />
          <IconButton aria-label="shopping cart" color="inherit">
            <Badge badgeContent={4} sx={{
              "& .MuiBadge-badge": {
                color: 'white',
                backgroundColor: '#c23908'
              }
            }}>
              <ShoppingCartIcon />
            </Badge>
          </IconButton>
          <Stack marginLeft={2} >
            <Typography variant='body2'>
              ShoppingCart:
            </Typography>
            <Typography variant="subtitle2">
              $57.00
            </Typography>
          </Stack>
        </Box>
      </Container>
      <Divider />
      <Container
        maxWidth
        sx={{
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'space-around',
          backgroundColor: '#FFA436',
          padding: 2
        }}
      >
        {isDesktop && <MenuDesktop isOffset={isOffset} isHome={isHome} navConfig={navConfig} />}

        <Box display='flex' alignItems='center'>
          <Button
            target="_blank"
            rel="noopener"
            href="https://material-ui.com/store/items/minimal-dashboard/"
            sx={{ color: '#c23908' }} // This sets the color of the icon in the button
          >
            <PhoneInTalkOutlinedIcon />
          </Button>
          <Typography sx={{ color: '#c23908' }}>(219) 555-0114</Typography>
        </Box>

        {!isDesktop && <MenuMobile isOffset={isOffset} isHome={isHome} navConfig={navConfig} />}
      </Container>
    </AppBar >
  );
}
